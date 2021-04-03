using System.Threading.Tasks;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        public Task<bool> IsFirstTrialSubscription(string subscriberId,string appId);
        
        public Task StoreSubscription(Subscription subscription);

        public Task<Subscription> LastSubscription(string subscriberId, string appId);
        public Task SetBlockingStatus(Subscription blocked);
        public Task<Subscription> GetSubscription(string id);
        public Task SetStatus(Subscription subscription);
        public Task AddCycle(Cycle cycle);
        public Task<Cycle> GetCycle(string cycleId);
        public Task SetCycleDateTimeRange(Cycle cycle);
    }
}