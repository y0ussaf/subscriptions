using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ExpiredOffer : Offer
    {
        public Expiration Expiration { get; set; }

        public ExpiredOffer(string id, Plan plan,Expiration expiration) : base(id, plan)
        {
            Expiration = expiration;
        }
    }
}