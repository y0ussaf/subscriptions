using System;

namespace Subscriptions.Domain.Entities
{
    public abstract class Subscription
    {
        public string Id { get; set; }
        public Offer Offer { get; set; }
        public string SubscriberId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public bool Active { get; set; }

        public virtual bool IsValid()
        {
            return Active;
        }
    }

    public enum SubscriptionType 
    {
        Trial,
        Paid,
        Free
    }
}