using System;

namespace Subscriptions.Domain.Entities
{
    public abstract class Cycle
    {
        protected Cycle(Subscription subscription)
        {
            Subscription = subscription;
            Offer = subscription.CurrentOffer;
        }
        
        public Subscription Subscription { get; set; }
        public Offer Offer { get; set; }
        public CycleType Type { get; set; }
        public abstract bool IsValid();
    }

    public enum CycleType
    {
        Paid,
        UnExpiredFree,
        ExpiredFree,
    }
}