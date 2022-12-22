using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Plans.GetPlan;
using Subscriptions.Application.Queries.Plans.GetPlans;
using PlanDto = Subscriptions.Application.Queries.Plans.GetPlan.PlanDto;

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
            var sql = @"select p.id, p.name, p.description, f.id , f.name, pf.details , o.id, o.name, o.description
                        from plan p inner join plan_feature pf on p.id = pf.plan_id
                        inner join feature f on pf.feature_id = f.id
                        inner join offer o on p.id = o.plan_id
                        where p.id = @id";
            var result = (await con.QueryAsync<PlanDto,FeatureDto,OfferDto,PlanDto>(sql,(plan,feature,offer) =>
                {
                    plan.Features.Add(feature);
                    plan.Offers.Add(offer);
                    return plan;
                } ,new { id }, _unitOfWorkContext.GetTransaction(), splitOn:"k")).ToList();
            
            var plan = result.FirstOrDefault();
            if (plan is null)
            {
                return null;
            }

            var features = new Dictionary<long,FeatureDto>();
            var offers = new Dictionary<long,OfferDto>();
            foreach (var r in result)
            {
                var feature = r.Features.Single();
                if (features.ContainsKey(feature.Id) is false)
                {
                    features.Add(feature.Id,feature);
                }

                var offer = r.Offers.Single();
                if (offers.ContainsKey(offer.Id) is false)
                {
                    offers.Add(offer.Id,offer);
                }
            }

            plan.Features = features.Values.ToList();
            plan.Offers = offers.Values.ToList();
            return plan;
        }
    }
}