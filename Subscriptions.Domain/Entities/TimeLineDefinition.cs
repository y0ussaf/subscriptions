using System;
using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class TimeLineDefinition
    {
        public string Id { get; set; }
        public TimelineDefinitionType TimeLineDefinitionType { get; set; }
        public int Order { get; set; }
        public Offer Offer { get; set; }
        public abstract IEnumerable<TimeLine> Build(DateTime now);
    }

    public enum TimelineDefinitionType
    {
        OneOrManyFinitePaidTimeLineDefinition,
        MonthlyFinitePaidTimeLineDefinition,
        InfiniteFreeTimeLineDefinition,
        InfinitePaidTimelineDefinition,
        FiniteFreeTimeLineDefinition
    }
}