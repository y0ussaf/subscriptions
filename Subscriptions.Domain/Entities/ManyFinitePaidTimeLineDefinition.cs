using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class ManyFinitePaidTimeLineDefinition : PaidTimeLineDefinition
    {
        public ManyFinitePaidTimeLineDefinition()
        {
            Type = TimelineDefinitionType.ManyFinitePaidTimeLineDefinition;
        }

        public int Repeat { get; set; }
        public Expiration Expiration { get; set; }
        public override FinitePaidTimeLine Build(DateTime now)
        {
            return new FinitePaidTimeLine(Expiration.CreateDateTimeRangeFromExpiration(now));
        }
    }
}