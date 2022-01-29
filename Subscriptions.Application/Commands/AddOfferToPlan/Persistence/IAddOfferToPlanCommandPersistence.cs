using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan.Persistence
{
    public interface IAddOfferToPlanCommandPersistence
    {
        Task<bool> OfferExist(long appId,string planName,string offerName);
        Task AddOffer(long appId,string planName,Offer offer);
    }
}