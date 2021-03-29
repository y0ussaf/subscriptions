using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidOffer : ExpiredOffer
    {
        public long Price { get; set; }
        public Expiration FreeCycle { get; set; }
        public bool AutoCharging { get; set; }

        public bool OfferFreeCycle { get; set; }
    }
}