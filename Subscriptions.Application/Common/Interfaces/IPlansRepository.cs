using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IPlansRepository
    {
        Task StorePlan(Plan plan);
        Task<Plan> GetPlan(string id);
        Task SetDefaultPlan(long appId,string planName);
        Task<bool> Exist(long appId, string planName);
        Task SetDefaultOffer(long appId, string planName, string offerName);
    }
}