using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FreeExpiredCycle : ExpiredCycle
    {
        public ExpiredOffer Offer { get; set; }

        public FreeExpiredCycle(Subscription subscription,DateTimeRange dateTimeRange) : base(subscription,dateTimeRange)
        {
            Type = CycleType.ExpiredFree;
        }
    }
}