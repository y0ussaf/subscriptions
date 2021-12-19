using System;
using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class TimeLineDefinition
    {
        public TimelineDefinitionType TimeLineDefinitionType { get; set; }

     

        public abstract IEnumerable<TimeLine> Build(DateTime now);
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