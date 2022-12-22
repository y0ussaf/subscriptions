using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Plans.GetPlans;
using Subscriptions.Application.Queries.Plans.GetPlans.Persistence;

namespace Subscriptions.Persistence.Queries.Plans.GetPlans
{
    public class GetPlansQueryPersistence : IGetPlansQueryPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetPlansQueryPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }
        public async Task<(IEnumerable<PlanDto>, long)> GetPlansWithCount(GetPlansQuery query)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var sql = @"select * from plan order by  id desc limit @limit offset @offset;
                        select count(*) from plan;
                      ";
            var grid =  await con.QueryMultipleAsync(sql, new
            {
                limit = query.PageSize,
                offset = (query.Page -1) * query.PageSize
            }, _unitOfWorkContext.GetTransaction());
            return (await grid.ReadAsync<PlanDto>(),await grid.ReadSingleAsync<long>() );
        }


    }
}