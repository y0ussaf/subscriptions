using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Interfaces
{
    public interface IOffersRepository
    {
        Task AddOfferToPlan(long appId , string planName ,Offer offer);
        Task<Offer> GetOfferByName(long appId,string planName,string offerName);
        Task SetDefaultOfferForPlan(string planId, string offerId);
        Task<Offer> GetOfferByNameIncludingTimelinesDefinitions(string appId, string planName, string offerName);
        Task<bool> Exist(long appId, string planName, string offerName);
    }
}