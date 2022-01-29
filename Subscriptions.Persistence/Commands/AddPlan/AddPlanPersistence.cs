using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Commands.AddPlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.AddPlan
{
    public class AddPlanPersistence : IAddPlanCommandPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public AddPlanPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<bool> AppExist(long appId)
        {            
            var sql = "select 1 from app where id = @id";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql, new {appId}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
        }

        public async Task AddPlan(long appId, Plan plan)
        {
            var sql = "insert into plan (app_id,name,description) values (@appId,@name,@description)";
            var con = _unitOfWorkContext.GetSqlConnection();
            await con.ExecuteAsync(sql, new
            {
                AppId = appId,
                plan.Description,
                plan.Name
            },_unitOfWorkContext.GetTransaction());        }
    }
}