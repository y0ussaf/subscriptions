using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json.Serialization;
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
            var orderBy = new SnakeCaseNamingStrategy().GetPropertyName(query.OrderBy, false);
            var sql = $@"select p.*, coalesce(t.total_subscriptions,0) total_subscriptions from plan p left join 
                        (
                            select count(*) as total_subscriptions, o.plan_id 
                            from subscription s inner join offer o on s.offer_id = o.id
                            group by o.plan_id
                        ) t
                        on p.id = t.plan_id
                        order by {orderBy} {(query.Asc ? "asc" : "desc")}  limit @limit offset @offset; 
                        select count(*) from plan;";
            var grid =  await con.QueryMultipleAsync(sql, new
            {
                limit = query.PageSize,
                offset = (query.Page -1) * query.PageSize,

            }, _unitOfWorkContext.GetTransaction());
            return (await grid.ReadAsync<PlanDto>(),await grid.ReadSingleAsync<long>() );
        }


    }
}