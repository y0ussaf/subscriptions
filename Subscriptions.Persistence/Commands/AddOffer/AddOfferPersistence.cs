using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Subscriptions.Application.Commands.AddOfferToPlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddOffer
{
    public class AddOfferPersistence : IAddOfferToPlanCommandPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddOfferPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<bool> OfferExist(long appId, string planName, string offerName)
        {
            var sql = "select 1 from offer where app_id = @appId and plan_name = @planName and name = @offerName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql,
                new {AppId = appId, PlanName = planName, OfferName = offerName}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
        }

        public async Task AddOffer(long appId, string planName, Offer offer)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var batch = new NpgsqlBatch(con);
            batch.BatchCommands.Add(new NpgsqlBatchCommand(
                "insert into offer (app_id, plan_name, name, description) values (@appId,@planName,@name,@description);"));
            foreach (var timeLineDefinition in offer.TimeLineDefinitions)
            {
                var batchCmd = new NpgsqlBatchCommand();
                switch (timeLineDefinition)
                {
                    case OneOrManyFinitePaidTimeLineDefinition oneOrManyExpiredPaidTimeLineDefinition:
                        batchCmd.CommandText = @"insert into timeline_definition (id,repeat, app_id, plan_name,
                                            offer_name,""order"",amount,minutes,hours,days,months,years,discriminator)
                                            values (@id,@repeat,@appId,@planName,@offerName,@order,@minutes,@hours,
                                            @days,@months,@years,@amount,@discriminator)";
                        batchCmd.Parameters.AddRange(
                            new object[]
                            {
                                ("repeat", oneOrManyExpiredPaidTimeLineDefinition.Repeat)
                            }
                        );
                        break;
                    case InfinitePaidTimelineDefinition:
                        batchCmd.CommandText = @"insert into timeline_definition (id,app_id, plan_name, offer_name,""order""
                                            , amount,discriminator) values (@id,@appId,@planName,@offerName,@order,@amount,@discriminator)";
                        break;
                    case FiniteFreeTimeLineDefinition:
                        batchCmd.CommandText = @"insert into timeline_definition (id,app_id, plan_name, offer_name,""order""
                                            ,minutes,hours,days,months,years,discriminator) 
                                            values (@id,@appId,@planName,@offerName,@order,@minutes,@hours,@days,@months
                                            ,@years,@discriminator)";
                        break;
                    case InfiniteFreeTimeLineDefinition:
                        batchCmd.CommandText = @"insert into timeline_definition (id,app_id, plan_name, offer_name,""order"",discriminator) 
                                            values (@id,@appId,@planName,@offerName,@order,@discriminator)";
                        break;
                }

                if (timeLineDefinition is PaidTimeLineDefinition paidTimeLineDefinition)
                {
                    batchCmd.Parameters.AddRange(new NpgsqlParameter []
                    {
                        new ("amount",paidTimeLineDefinition.Amount),
                        new ("auto_charging",paidTimeLineDefinition.AutoCharging)
                    });
                }


                if (timeLineDefinition is IFiniteTimeLineDefinition finiteTimeLineDefinition)
                {
                    batchCmd.Parameters.AddRange(new NpgsqlParameter []
                    {
                        new ("minutes", finiteTimeLineDefinition.Expiration.Minutes),
                        new ("hours", finiteTimeLineDefinition.Expiration.Hours),
                        new ("days", finiteTimeLineDefinition.Expiration.Days),
                        new ("months", finiteTimeLineDefinition.Expiration.Months),
                        new ("years", finiteTimeLineDefinition.Expiration.Years)
                    });
                }
                batchCmd.Parameters.AddRange(new NpgsqlParameter []
                {
                    new ("id",timeLineDefinition.Id),
                    new ("appId", appId),
                    new ("planName",planName),
                    new ("offerName",offer.Name),
                    new ("order",timeLineDefinition.Order),
                    new ("discriminator",(int) timeLineDefinition.TimeLineDefinitionType),
                });
                
            }

            await batch.ExecuteNonQueryAsync();

        }
    }
}