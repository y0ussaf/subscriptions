using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class FiniteFreeTimeLineDefinition : TimeLineDefinition
    {
        public Expiration Expiration { get; set; }
        public FiniteFreeTimeLineDefinition()
        {
            Type = TimelineDefinitionType.FiniteFreeTimeLineDefinition;
        }
        public override FiniteFreeTimeLine Build(DateTime now)
        {
            var dateTimeRange = Expiration.CreateDateTimeRangeFromExpiration(now);
            return new FiniteFreeTimeLine(dateTimeRange);
        }

        
    }
}