using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface ISubscriptionsRepository
    {
        public Task<bool> IsFirstTrialSubscription(string subscriberId,string appId);
        
        public Task CreateSubscription(Subscription subscription);

        public Task<Subscription> LastSubscription(string subscriberId, string appId);
    }
}