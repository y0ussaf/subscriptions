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

        public async Task<Offer> GetOffer(long id)
        {
            var sql = @"select o.name,o.description,t.*
                        from offer as o inner join timeline_definition as t on
                        o.id = t.offer_id
                        where o.id = @id";
            var con = _unitOfWorkContext.GetSqlConnection();
            var rows = (await con.QueryAsync<GetOfferContract>(sql, new
            {
                id
            })).ToList();
            if (rows.Any())
            {
                var offer = new Offer();
                var firstRow = rows.First();
                _mapper.Map(firstRow, offer);
                foreach (var row in rows)
                {
                    offer.AddIntervalDefinition(_mapper.Map<IntervalDefinition>(row));
                }

                return offer;
            }

            return null;
        }

        public async Task<long> AddSubscription(Subscription subscription)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var batch = new NpgsqlBatch(con);
            var insertSubCmd = new NpgsqlBatchCommand()
            {
                CommandText = @"insert into subscription (subscriber_id,status) 
                                values (@subscriberId,@status)"
            };
             insertSubCmd.Parameters.AddRange(new object []
             {
                 ("subscriberId",subscription.Subscriber.Id),
                 ("status",subscription.Status.ToString())
             });

            foreach (var interval in subscription.Intervals)
            {
                var insertIntervalCmd = new NpgsqlBatchCommand
                {
                    CommandText = @"insert into timeline (during,timeline_definition_id
                        , subscription_id,price) 
                        values ('[@start,@end]', @timeline_definition_id ,currval(subscription_id_seq)
                        , @price)"
                };

                var intervalEnd = interval.DateTimeRange.End;
                insertIntervalCmd.Parameters.AddRange(new object []
                    {
                        ("timeline_definition_id",interval.IntervalDefinition.Id), 
                        ("start",interval.DateTimeRange.Start.ToString("h:mm:ss tt zz")),
                        ("end",( intervalEnd == null
                            ? "infinity"
                            : interval.DateTimeRange.End?.ToString("h:mm:ss tt zz")!) ?? string.Empty),
                        ("price",interval.IntervalDefinition.Price)
                    });
                
                batch.BatchCommands.Add(new NpgsqlBatchCommand()
                {
                    CommandText = "select currval(subscription_id_seq)"
                });
                batch.BatchCommands.Add(insertIntervalCmd);     

            }

            return (long) (await batch.ExecuteScalarAsync())!;
        }


    }
}