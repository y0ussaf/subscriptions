using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subscriptions.Application.Queries.Plans.GetPlans.Persistence
{
    public interface IGetPlansQueryPersistence
    {
        public Task<(IEnumerable<PlanDto>, long)> GetPlansWithCount(GetPlansQuery query);

    }
}