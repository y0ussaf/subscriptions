using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface ISubscribersRepository
    {
        public Task StoreSubscriber(Subscriber subscriber);
        public Task<Subscriber> GetSubscriber(string id);
        public Task SetDefaultPaymentMethod(Subscriber subscriber);
        public Task<bool> Exist(string appId, string subscriberId);
    }
}