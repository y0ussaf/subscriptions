using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class SubscriptionsRepository : ISubscriptionsRepository
    {
        public Task<bool> IsFirstTrialSubscription(string subscriberId, string appId)
        {
            throw new System.NotImplementedException();
        }

        public Task StoreSubscription(Subscription subscription)
        {
            throw new System.NotImplementedException();
        }

        public Task<Subscription> LastSubscription(string subscriberId, string appId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetBlockingStatus(Subscription blocked)
        {
            throw new System.NotImplementedException();
        }

        public Task<Subscription> GetSubscription(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task SetStatus(Subscription subscription)
        {
            throw new System.NotImplementedException();
        }

        public Task AddTimeline(string subscriptionId, TimeLine timeLine)
        {
            throw new System.NotImplementedException();
        }

        public Task<TimeLine> GetTimeline(string timelineId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetCycleDateTimeRange(TimeLine timeLine)
        {
            throw new System.NotImplementedException();
        }
    }
}