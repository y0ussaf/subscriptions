using System.Threading.Tasks;

namespace Subscriptions.Application.Queries.Plans.GetPlan
{
    public interface IGetPlanPersistence
    {
        Task<PlanDto> GetPlan(long id);
    }
}