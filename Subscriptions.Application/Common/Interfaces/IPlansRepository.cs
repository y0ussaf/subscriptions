using System.Threading.Tasks;
using Subscriptions.Application.Commands.UpdatePlan;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IPlansRepository
    {
        Task StorePlan(long appId, Plan plan);
        Task<Plan> GetPlanByName(long appId,string planName);
        Task<bool> Exist(long appId, string planName);
        Task SetDefaultOffer(long appId, string planName, string offerName);
        Task UpdatePlan(long appId,string planName, Plan plan);
    }
}