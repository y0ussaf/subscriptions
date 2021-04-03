using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class TrialOffer : ExpiredOffer
    {
        public bool TrialRequireCreditCard { get; set; }

        public TrialOffer(string id, Plan plan, Expiration expiration) : base(id, plan, expiration)
        {
            OfferType = OfferType.Trial;
        }
    }
}