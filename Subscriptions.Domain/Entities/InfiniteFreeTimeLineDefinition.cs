using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class InfiniteFreeTimeLineDefinition : TimeLineDefinition
    {
        public InfiniteFreeTimeLineDefinition()
        {
            Type = TimelineDefinitionType.InfiniteFreeTimeLineDefinition;
        }
        public override InfiniteFreeTimeLine Build(DateTime now)
        {
            return new InfiniteFreeTimeLine(new DateTimeRange(now,null));
        }
    }
}