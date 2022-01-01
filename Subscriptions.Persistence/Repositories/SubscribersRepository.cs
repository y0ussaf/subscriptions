using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class SubscribersRepository : ISubscribersRepository
    {
        public Task StoreSubscriber(Subscriber subscriber)
        {
            throw new System.NotImplementedException();
        }

        public Task<Subscriber> GetSubscriber(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task SetDefaultPaymentMethod(Subscriber subscriber)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Exist(long appId, string subscriberId)
        {
            throw new System.NotImplementedException();

        }
    }
}