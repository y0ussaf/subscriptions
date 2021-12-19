using System;
using System.Linq;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;
using Xunit;

namespace Subscriptions.Domain.Tests.Entities
{
    public class MonthlyFinitePaidTimeLineDefinitionTests
    {
        [Fact]

        public void Build_Many_Finite_Paid_Timelines_Starting_From_Specific_Day()
        {
            var monthExpiration = new TimelineExpiration(1, TimeIn.Months);
            var nbrOfTimelines = 3;
            var definition = new MonthlyExpiredPaidTimeLineDefinition(nbrOfTimelines,monthExpiration)
            {
                StartingDay = 5
            }; 
            var now = new DateTime(2021,12,10);
            var timeLines = definition.Build(now).ToList();
            // Count
            Assert.Equal(timeLines.Count, nbrOfTimelines);
            // Datetime Range of each timeline
            var start = new DateTime(2021,12,5);
            foreach (var timeline in timeLines)
            {
                Assert.Equal(timeline.DateTimeRange.Start,start);
                var end = start.AddMonths(1);
                Assert.Equal(timeline.DateTimeRange.End,end);
                start = end;
            }
        }
    }
}