using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class InfinitePaidTimeLine : PaidTimeLine,IInfiniteTimeLine
    {
        public InfinitePaidTimeLine(DateTime start, Invoice invoice) : base(new DateTimeRange(start,null),invoice)
        {
            TimelineType = TimelineType.InfinitePaidTimeline;
        }


        public void MakeItFinite(DateTime end)
        {
            DateTimeRange = new DateTimeRange(DateTimeRange.Start, end);
        }
    }
}