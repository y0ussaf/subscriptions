using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Offers.GetOffer.Persistence
{
    public interface IGetOfferQueryPersistence
    {
        public Task<Offer> GetOffer(long id);
    }
}