using System.Threading.Tasks;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Repositories
{
    public class OffersRepository : IOffersRepository
    {
        public Task AddOfferToPlan(long appId, string planName, Offer offer)
        {
            throw new System.NotImplementedException();
        }

        public Task<Offer> GetOfferByName(long appId, string planName, string offerName)
        {
            throw new System.NotImplementedException();
        }

        public Task SetDefaultOfferForPlan(string planId, string offerId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Offer> GetOfferByNameIncludingTimelinesDefinitions(long appId, string planName, string offerName)
        {
            throw new System.NotImplementedException();
        }
    }
}