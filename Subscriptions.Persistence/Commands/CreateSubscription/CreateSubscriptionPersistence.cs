using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Npgsql;
using Subscriptions.Application.Commands.CreateSubscription.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;
using Subscriptions.Persistence.Commands.CreateSubscription.Contracts;

namespace Subscriptions.Persistence.Commands.CreateSubscription
{
    public class CreateSubscriptionPersistence : ICreateSubscriptionCommandPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IMapper _mapper;

        public CreateSubscriptionPersistence(IUnitOfWorkContext unitOfWorkContext,IMapper mapper)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _mapper = mapper;
        }

        public async Task<Offer> GetOffer(string planName, string offerName)
        {
            var sql = @"select o.name,o.description,t.*
                        from offer as o inner join timeline_definition as t on
                        o.app_id = t.app_id and o.plan_name = t.plan_name and o.name = t.offer_name
                        where o.app_id = @appId and o.name = @planName and o.name = @offerName";
            var con = _unitOfWorkContext.GetSqlConnection();
            var rows = (await con.QueryAsync<GetOfferContract>(sql, new
            {
                planName,
                offerName
            })).ToList();
            if (rows.Any())
            {
                var offer = new Offer();
                var firstRow = rows.First();
                _mapper.Map(firstRow, offer);
                foreach (var row in rows)
                {
                    switch (row.Discriminator)
                    {
                        case TimelineDefinitionType.InfinitePaidTimelineDefinition :
                            offer.AddTimelineDefinition(_mapper.Map<InfinitePaidTimelineDefinition>(row));
                            break;
                        case TimelineDefinitionType.FiniteFreeTimeLineDefinition :
                            offer.AddTimelineDefinition(_mapper.Map<FiniteFreeTimeLineDefinition>(row));
                            break;
                        case TimelineDefinitionType.OneOrManyFinitePaidTimeLineDefinition:
                            offer.AddTimelineDefinition(_mapper.Map<OneOrManyFinitePaidTimeLineDefinition>(row));
                            break;
                        case TimelineDefinitionType.MonthlyFinitePaidTimeLineDefinition:
                            break;
                        case TimelineDefinitionType.InfiniteFreeTimeLineDefinition:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return offer;
            }

            return null;
        }

        public async Task AddSubscription(string planName, string offerName, Subscription subscription)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var insertSubSql = @"insert into subscription (id,subscriber_id, plan_name, offer_name) 
                                values (@id,@subscriberId,@planName,@offerName)";
            await con.ExecuteAsync(insertSubSql, new
            {
                subscription.Id,
                planName,
                offerName,
                subscriberId = subscription.Subscriber.Id
            });

            var batch = new NpgsqlBatch(con);

            foreach (var timeLine in subscription.TimeLines)
            {
                var insertTimelineCmd = new NpgsqlBatchCommand();

                insertTimelineCmd.CommandText = @"insert into timeline (id, range, subscription_id,
                  amount, auto_charging, discriminator) values (@id,'[@start,@end]',@subscriptionId,@amount
                  ,@autoCharging,@discriminator)";

                insertTimelineCmd.Parameters.AddWithValue("end",
                    (timeLine is IInfiniteTimeLine
                        ? "infinity"
                        : timeLine.DateTimeRange.End?.ToString("h:mm:ss tt zz")!) ?? string.Empty);

                if (timeLine is PaidTimeLine paidTimeLine)
                {
                    insertTimelineCmd.Parameters.AddWithValue(new NpgsqlParameter[]
                    {
                        new ("amount",paidTimeLine.Amount),
                        new ("autoCharging",paidTimeLine.AutoCharging),
                    });
                }
                    
                insertTimelineCmd.Parameters.AddWithValue(new NpgsqlParameter[]
                {
                    new ("start",timeLine.DateTimeRange.Start.ToString("h:mm:ss tt zz")),
                    new ("subscription_id",subscription.Id),
                    new ("discriminator",timeLine),
                });
            }

            await batch.ExecuteNonQueryAsync();
        }

        public Task<bool> SubscriberExist(string subscriberId)
        {
            throw new NotImplementedException();
        }
    }
}