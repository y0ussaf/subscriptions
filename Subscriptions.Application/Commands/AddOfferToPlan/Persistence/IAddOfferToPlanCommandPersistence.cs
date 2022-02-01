using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan.Persistence
{
    public interface IAddOfferToPlanCommandPersistence
    {
        Task<bool> OfferExist(string planName,string offerName);
        Task AddOffer(string planName,Offer offer);
    }
}