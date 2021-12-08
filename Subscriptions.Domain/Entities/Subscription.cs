using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public  class Subscription
    {
        public Subscription(string id, Subscriber subscriber)
        {
            Id = id;
            Subscriber = subscriber;
            Status = SubscriptionStatus.Active;
            Blocked = false;
        }
        public Offer Offer { get; set; }
        public List<TimeLine> TimeLines { get; set; }
        public bool AllPaidCyclesShouldBePaid { get; set; }
        public bool CreatingNextPaidCycleAutomatically { get; set; }
        public string Id { get; set; }
        public Subscriber Subscriber { get; set; }
        public SubscriptionStatus Status { get; set; }
        public bool Blocked { get; set; }

        public bool IsValid(DateTime now)
        {
            throw new NotImplementedException();
        }

        public bool IsActive()
        {
            return Status == SubscriptionStatus.Active;
        }
    }

    public enum SubscriptionStatus
    {
        Canceled,
        Paused,
        Active
    }
}