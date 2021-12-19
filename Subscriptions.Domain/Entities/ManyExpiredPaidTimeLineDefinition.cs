using System;
using System.Collections.Generic;
using System.Diagnostics;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ManyExpiredPaidTimeLineDefinition : PaidTimeLineDefinition,IFiniteTimeLineDefinition
    {
        public ManyExpiredPaidTimeLineDefinition(int repeat,TimelineExpiration expiration)
        {
            Repeat = repeat;
            Expiration = expiration;
            TimeLineDefinitionType = TimelineDefinitionType.ManyFinitePaidTimeLineDefinition;
        }

        public int Repeat { get; set; }
        public TimelineExpiration Expiration { get; set; }
        public override IEnumerable<FinitePaidTimeLine> Build(DateTime now)
        {
            var timelines = new List<FinitePaidTimeLine>();
            var nextTimelineStart = now;
            for (var i = 0; i < Repeat; i++)
            {
                var invoice = new Invoice(Guid.NewGuid().ToString(), InvoiceStatus.WaitingToBePaid,AutoCharging);
                var timeline = new FinitePaidTimeLine(Expiration.CreateDateTimeRangeFromExpiration(nextTimelineStart),invoice);
                timelines.Add(timeline);
                Debug.Assert(timeline.DateTimeRange.End != null, "timeline.DateTimeRange.End != null");
                nextTimelineStart = (DateTime) timeline.DateTimeRange.End;
            }

            return timelines;
        }
    }
}