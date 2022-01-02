using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Commands.UpdatePlan;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class PlansRepository : IPlansRepository
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public PlansRepository(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task StorePlan(long appId, Plan plan)
        {
            var sql = "insert into plan (\"appId\",name,description) values (@appId,@name,@description)";
            var con = _unitOfWorkContext.GetSqlConnection();
            await con.ExecuteAsync(sql, new
            {
                AppId = appId,
                plan.Description,
                plan.Name
            },_unitOfWorkContext.GetTransaction());
            
        }

        public async Task<Plan> GetPlanByName(long appId,string planName)
        {
            var sql = "select * from plan where \"appId\" = @appId and name = @planName";
            var con = _unitOfWorkContext.GetSqlConnection();
            return await con.QueryFirstOrDefaultAsync<Plan>(sql, new {AppId = appId, PlanName = planName});
        }

        public Task SetDefaultPlan(long appId, string planName)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Exist(long appId, string planName)
        {
            var sql = "select 1 from plan where \"appId\" = @appId and name = @planName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await using var reader = await con.ExecuteReaderAsync(sql, new {AppId = appId,PlanName = planName}, _unitOfWorkContext.GetTransaction());
            return reader.HasRows;
            
        }

        public async Task SetDefaultOffer(long appId, string planName, string offerName)
        {
            var sql = "update plan set defaultOfferName = @offerName where \"appId\" = @appId and name = '@planName'";
            var con = _unitOfWorkContext.GetSqlConnection();
            await con.ExecuteAsync(sql, new {AppId = appId, PlaneName = planName,OfferName = offerName});
        }

        public async Task UpdatePlan(long appId, string planName, Plan plan)
        {
            var sql = "update plan set name = @newName, description = @description where \"appId\" = @appId and name = @planName";
            var con = _unitOfWorkContext.GetSqlConnection();
            await con.ExecuteAsync(sql,new {NewName = plan.Name,PlanName = planName,AppId = appId,plan.Description},_unitOfWorkContext.GetTransaction());
        }
    }
}