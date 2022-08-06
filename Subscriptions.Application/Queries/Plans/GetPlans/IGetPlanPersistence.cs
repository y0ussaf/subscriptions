using System.Threading.Tasks;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public interface IGetPlanPersistence
    {
        Task<PlanDto> GetPlan(long id);
    }
}