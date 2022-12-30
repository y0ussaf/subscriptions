using System.Collections.Generic;
using System.Threading.Tasks;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Offers.GetOffers.Persistence
{
    public interface IGetOffersQueryPersistence
    {
        public Task<(IEnumerable<OfferDto>, long)> GetOffersWithCount(GetOffersQuery query);
    }
}