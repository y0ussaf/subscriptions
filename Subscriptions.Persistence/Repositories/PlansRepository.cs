using System.Threading.Tasks;
using Dapper;
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

        public Task<bool> Exist(long appId, string planName)
        {
            throw new System.NotImplementedException();
        }

        public Task SetDefaultOffer(long appId, string planName, string offerName)
        {
            throw new System.NotImplementedException();
        }
    }
}