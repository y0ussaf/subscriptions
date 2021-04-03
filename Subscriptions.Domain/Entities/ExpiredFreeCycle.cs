using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ExpiredFreeCycle : Cycle
    {

        public ExpiredFreeCycle(PaidSubscription subscription,DateTimeRange dateTimeRange) : base(subscription,dateTimeRange)
        {
            Type = CycleType.ExpiredFree;
        }
        
    }
}