using System;
using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class MonthlyExpiredPaidTimeLineDefinition : ManyExpiredPaidTimeLineDefinition
    {
        public MonthlyExpiredPaidTimeLineDefinition(int repeat, TimelineExpiration expiration) : base(repeat, expiration)
        {
            TimeLineDefinitionType = TimelineDefinitionType.MonthlyFinitePaidTimeLineDefinition;

        }

        public int StartingDay { get; set; } = 1;
        public override IEnumerable<FinitePaidTimeLine> Build(DateTime now)
        {
            var start = new DateTime(now.Year, now.Month, StartingDay);
            return base.Build(start);
        }
    }
}