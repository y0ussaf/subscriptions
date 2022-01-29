using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FinitePaidTimeLine : PaidTimeLine
    {

        public FinitePaidTimeLine(DateTimeRange dateTimeRange,Invoice invoice) : base(dateTimeRange,invoice)
        {
            TimelineType = TimelineType.FinitePaidTimeline;
        }

     
    }
}