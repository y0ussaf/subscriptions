using System;

namespace Subscriptions.Domain.Entities
{
    public class Subscription
    {
        public Plan Plan { get; set; }
        public string SubscriberId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsTrial { get; set; }
    }
}