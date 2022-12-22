using MediatR;
using Subscriptions.Application.Queries.Plans.GetPlans;

namespace Subscriptions.Application.Queries.Plans.GetPlan
{
    public class GetPlanQuery : IRequest<GetPlanQueryResponse>
    {
        public long PlanId { get; set; }
    }
}