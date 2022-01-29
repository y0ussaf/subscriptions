using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddFeatureToPlan.Persistence
{
    public interface IAddFeatureToPlanCommandPersistence
    {
        public Task AddFeatureToPlan(long appId, string planName, Feature feature);
        Task<bool> PlanExist(long appId, string planName);
    }
}