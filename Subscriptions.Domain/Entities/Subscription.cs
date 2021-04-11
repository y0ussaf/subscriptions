using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public abstract class Subscription
    {
        public Subscription(string id, Subscriber subscriber)
        {
            Id = id;
            Subscriber = subscriber;
            Status = SubscriptionStatus.Active;
            Blocked = false;
        }

        public string Id { get; set; }
        public Subscriber Subscriber { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public SubscriptionStatus Status { get; set; }
        public SubscriptionType Type { get; set; }
        public bool Blocked { get; set; }
        public abstract bool IsValid(DateTime now);
        public bool IsActive()
        {
            return Status == SubscriptionStatus.Active;
        }
    }

    public enum SubscriptionType 
    {
        Trial,
        Paid,
        Free
    }

    public enum SubscriptionStatus
    {
        Canceled,
        Paused,
        Active
    }
}