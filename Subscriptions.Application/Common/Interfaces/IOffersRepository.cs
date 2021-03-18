using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IOffersRepository
    {
        Task AddOfferToPlan(Offer offer);
        Task<Offer> GetOffer(string offerId);
        Task SetDefaultOfferForPlan(string planId, string offerId);
    }
}