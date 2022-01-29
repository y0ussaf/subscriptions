using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Subscriptions.Application.Commands.AddFeatureToPlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanPersistence : IAddFeatureToPlanCommandPersistence 
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddFeatureToPlanPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task AddFeatureToPlan(long appId, string planName, Feature feature)
        {
            var sql = @"insert into feature (app_id, plan_name, description) values (@appId, @planName, @description)";
            await using var cmd = new NpgsqlCommand(sql)
            {
                Parameters =
                {
                    new ("appId",appId),
                    new ("planName",planName),
                    new ("description",feature.Description)
                }
            };
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> PlanExist(long appId, string planName)
        {
            var sql = "select 1 from plan where app_id = @appId and name = @planName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql, new {AppId = appId,PlanName = planName}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
            
        }
    }
}