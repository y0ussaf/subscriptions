using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class InfinitePaidTimelineDefinition : PaidTimeLineDefinition
    {
        public InfinitePaidTimelineDefinition()
        {
            Type = TimelineDefinitionType.InfinitePaidTimelineDefinition;
        }

        public override TimeLine Build(DateTime now)
        {
            return new InfinitePaidTimeLine(new DateTimeRange(now, null));
        }
    }
}