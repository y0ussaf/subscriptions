using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public  class Subscription
    {
        public Subscription(string id,Offer currentOffer,string subscriberId)
        {
            Id = id;
            Status = SubscriptionStatus.Active;
            Cycles = new List<Cycle>();
            CurrentOffer = currentOffer;
            SubscriberId = subscriberId;
            Blocked = false;
        }

        public string Id { get; set; }
        public string SubscriberId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public List<Cycle> Cycles { get; set; } = new();
        public SubscriptionStatus Status { get; set; }
        public SubscriptionType Type { get; set; }
        public Offer CurrentOffer { get; set; }
        public bool Blocked { get; set; }
        public bool IsValid()
        {
            return Cycles.Last().IsValid();
        }
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