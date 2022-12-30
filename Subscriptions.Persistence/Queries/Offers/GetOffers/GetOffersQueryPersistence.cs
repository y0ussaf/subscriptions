using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json.Serialization;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Offers.GetOffers;
using Subscriptions.Application.Queries.Offers.GetOffers.Persistence;

namespace Subscriptions.Persistence.Queries.Offers.GetOffers
{
    public class GetOffersQueryPersistence : IGetOffersQueryPersistence
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetOffersQueryPersistence(IUnitOfWorkContext unitOfWorkContext)
        {
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<(IEnumerable<OfferDto>, long)> GetOffersWithCount(GetOffersQuery query)
        {
            var con = _unitOfWorkContext.GetSqlConnection();
            var orderBy = new SnakeCaseNamingStrategy().GetPropertyName(query.OrderBy, false);
            var sql = $@"select o.*, coalesce(t.total_subscriptions,0) total_subscriptions from offer o left join 
                        (
                            select count(*) as total_subscriptions, offer_id 
                            from subscription
                            group by offer_id
                        ) t
                        on o.id = t.offer_id
                        where o.plan_id = @planId
                        order by {orderBy} {(query.Asc ? "asc" : "desc")}  limit @limit offset @offset; 
                        select count(*) from offer where plan_id = @planId;";
            var grid =  await con.QueryMultipleAsync(sql, new
            {
                limit = query.PageSize,
                offset = (query.Page -1) * query.PageSize,
                planId = query.PlanId
            }, _unitOfWorkContext.GetTransaction());
            return (await grid.ReadAsync<OfferDto>(),await grid.ReadSingleAsync<long>() );
        }
    }
}