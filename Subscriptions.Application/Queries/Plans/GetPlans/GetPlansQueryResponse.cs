using System.Collections.Generic;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public class GetPlansQueryResponse
    {
        
        
        public IEnumerable<PlanDto> Plans { get; set; }
        public long Count { get; set; }
    }
}