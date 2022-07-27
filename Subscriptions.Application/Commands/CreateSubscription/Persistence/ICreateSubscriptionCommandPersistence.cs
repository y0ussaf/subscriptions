using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateSubscription.Persistence
{
    public interface ICreateSubscriptionCommandPersistence
    {
        Task<Offer> GetOffer(long id);
        Task AddSubscription(Subscription subscription);
        Task<bool> SubscriberExist(string subscriberId);
    }
}