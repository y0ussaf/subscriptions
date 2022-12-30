using System.Collections.Generic;

namespace Subscriptions.Application.Queries.Offers.GetOffers
{
    public class GetOffersQueryResponse 
    {
        public IEnumerable<OfferDto> Offers { get; set; }
        public long Count { get; set; }
    }
}