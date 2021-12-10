using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class TimeLineDefinition
    {
        public TimelineDefinitionType Type { get; set; }

     

        public abstract TimeLine Build(DateTime now);
    }

    public enum TimelineDefinitionType
    {
        ManyFinitePaidTimeLineDefinition,
        MonthlyFinitePaidTimeLineDefinition,
        InfiniteFreeTimeLineDefinition,
        InfinitePaidTimelineDefinition,
        FiniteFreeTimeLineDefinition
    }
}