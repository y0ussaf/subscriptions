using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ExpiredOffer : Offer
    {
        public Expiration Expiration { get; set; }
    }
}