using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateSubscription.Persistence
{
    public interface ICreateSubscriptionCommandPersistence
    {
        Task<Offer> GetOffer(long appId, string planName, string offerName);
        Task AddSubscription(string appId, string planName, string offerName, Subscription subscription);
    }
}