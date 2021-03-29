using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IPlansRepository
    {
        Task StorePlan(Plan plan);
        Task<Plan> GetPlan(string id);
        Task SetDefaultPlan(string appId,string planId);
    }
}