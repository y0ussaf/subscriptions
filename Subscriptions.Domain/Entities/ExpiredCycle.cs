using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ExpiredCycle : Cycle
    {
        public DateTimeRange DateTimeRange { get; set; }
        public override bool IsValid()
        {
            return DateTimeRange.Contains(DateTime.Now);
        }

        public ExpiredCycle(Subscription subscription,DateTimeRange dateTimeRange) : base(subscription)
        {
            DateTimeRange = dateTimeRange;
        }

         
    }
}