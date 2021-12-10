using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class MonthlyFinitePaidTimeLineDefinition : ManyFinitePaidTimeLineDefinition
    {
        public MonthlyFinitePaidTimeLineDefinition()
        {
            Type = TimelineDefinitionType.MonthlyFinitePaidTimeLineDefinition;
        }

        public int StartingDay { get; set; } = 1;
        public override FinitePaidTimeLine Build(DateTime now)
        {
            return new FinitePaidTimeLine(Expiration.CreateDateTimeRangeFromExpiration(now));
        }
    }
}