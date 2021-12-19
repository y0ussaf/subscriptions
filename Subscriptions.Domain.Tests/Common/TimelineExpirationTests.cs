using System;
using Subscriptions.Domain.Common;
using Xunit;

namespace Subscriptions.Domain.Tests.Common
{
    public class TimelineExpirationTests
    {

        [Fact]
        public void Create_DatetimeRange_From_Expiration_In_Days()
        {
            var timelineExpiration = new TimelineExpiration(5, TimeIn.Days);
            var now = new DateTime(2021,11,28,15,5,12);
            var dateTimeRange = timelineExpiration.CreateDateTimeRangeFromExpiration(now);
            Assert.Equal(dateTimeRange.Start,now);
            Assert.Equal(dateTimeRange.End,new DateTime(2021,12,3,15,5,12));
        }

        [Fact]
        public void Create_DatetimeRange_From_Expiration_In_Months()
        {
            var timelineExpiration = new TimelineExpiration(1, TimeIn.Months);
            var now = new DateTime(2021,12,28);
            var dateTimeRange = timelineExpiration.CreateDateTimeRangeFromExpiration(now);
            Assert.Equal(dateTimeRange.Start,now);
            Assert.Equal(dateTimeRange.End,new DateTime(2022,01,28));
        }
    }
}