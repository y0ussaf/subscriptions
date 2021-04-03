using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class Cycle
    {
        protected Cycle(PaidSubscription subscription,DateTimeRange dateTimeRange)
        {
            Subscription = subscription;
            DateTimeRange = dateTimeRange;
        }
        public DateTimeRange DateTimeRange { get; set; }

        public PaidSubscription Subscription { get; set; }
        public CycleType Type { get; set; }

        public virtual bool IsValid()
        {
            return DateTimeRange.Contains(DateTime.Now);
        }
        public DateTime CreatedAt { get; set; }
    }

    public enum CycleType
    {
        Paid,
        ExpiredFree,
    }
}