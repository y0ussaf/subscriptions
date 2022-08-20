using System;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities.Conditions;

namespace Subscriptions.Domain.Entities
{
    public abstract class TimeLine
    {
        public string Id { get; set; }
        protected TimeLine(DateTimeRange dateTimeRange)
        {
            DateTimeRange = dateTimeRange;
        }
        public TimeLineDefinition TimeLineDefinition { get; set; }
        public abstract bool IsValid(DateTime date);
        public DateTimeRange DateTimeRange { get; set; }
        public TimelineType TimelineType { get; set; }
        public LogicalExpressionsGroup ConditionsGroup { get; set; }
    }

    public enum TimelineType
    {
        FinitePaidTimeline,
        InfinitePaidTimeline,
        FiniteFreeTimeline,
        InfiniteFreeTimeline
    }
    
}