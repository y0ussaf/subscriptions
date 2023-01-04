
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Plans.GetPlan;

namespace Subscriptions.Persistence.Queries.Plans.GetPlan
{
    public class GetPlanQueryPersistence : IGetPlanPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetPlanQueryPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<PlanDto> GetPlan(long id)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var sql = @"select * from plan where id = @id";
            return await con.QueryFirstOrDefaultAsync<PlanDto>(sql
                , new { id }, _unitOfWorkContext.GetTransaction());
        }
    }
}