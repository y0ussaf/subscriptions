using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ExpiredSubscription : Subscription
    {
        public DateTimeRange DateTimeRange { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() && DateTimeRange.Contains(DateTime.Now);
        }
    }
}