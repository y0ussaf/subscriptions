using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateSubscription.Persistence
{
    public interface ICreateSubscriptionCommandPersistence
    {
        Task<Offer> GetOffer(string planName, string offerName);
        Task AddSubscription(string planName, string offerName, Subscription subscription);
        Task<bool> SubscriberExist(string subscriberId);
    }
}