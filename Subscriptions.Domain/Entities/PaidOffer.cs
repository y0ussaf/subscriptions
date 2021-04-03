using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidOffer : ExpiredOffer
    {
        
        public long Price { get; set; }
        public Expiration FreeCycleExpiration { get; set; }
        public bool AutoCharging { get; set; }
        public bool AllPaidCyclesShouldBePaid { get; set; }
        public bool OfferFreeCycle { get; set; }

        public PaidOffer(string id, Plan plan, long price,Expiration expiration,Expiration freeCycleExpiration = null) : base(id, plan, expiration)
        {
            Price = price;
            OfferType = OfferType.Paid;
            OfferFreeCycle = freeCycleExpiration is not null;
            AutoCharging = true;
            AllPaidCyclesShouldBePaid = false;
        }

        public virtual PaidCycle CreatePaidCycle(PaidSubscription subscription,Invoice invoice,DateTime now)
        {
            return new(subscription, Expiration.CreateDateTimeRangeFromExpiration(now), invoice);
        }

        public virtual ExpiredFreeCycle CreateOfferedFreeCycle(PaidSubscription subscription, DateTime now)
        {
            return new(subscription, FreeCycleExpiration.CreateDateTimeRangeFromExpiration(now));
        }

        public virtual ExpiredFreeCycle CreateFreeExpiredCycle(PaidSubscription subscription,Expiration expiration,DateTime now)
        {
            return new(subscription, expiration.CreateDateTimeRangeFromExpiration(now));
        }

        public virtual ExpiredFreeCycle CreateExpiredFreeCycleWithUndefinedEnd(PaidSubscription subscription, DateTime now)
        {
            return new(subscription, new DateTimeRange(now,null));
        }
    }

    public enum FirstCycleStartingTime
    {
        AtCreation,
        Specific 
        
    }
}