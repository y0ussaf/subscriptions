using System;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;
using Xunit;

namespace Subscriptions.Domain.Tests.Entities
{
    public class TimelinesTests 

    {

        [Fact]
        public void FiniteFreeTimeLine_Is_Valid_If_Unexpired()
        {
            var expiration = new TimelineExpiration(1, TimeIn.Hours);
            var now = new DateTime(2021, 12, 01);
            var finiteFreeTimeLine = new FiniteFreeTimeLine(expiration.CreateDateTimeRangeFromExpiration(now));
            Assert.True(finiteFreeTimeLine.IsValid(new DateTime(2021,12,01,0,25,0)));
            Assert.False(finiteFreeTimeLine.IsValid(new DateTime(2021,12,01,1,0,0)));
        }
        [Fact]
        public void InfiniteFreeTimeLine_Is_Valid_Until_You_Make_It_Expired()
        {
            var start = new DateTime(2021, 12, 01);
            var infiniteFreeTimeLine = new InfiniteFreeTimeLine(start);
            var date1 = new DateTime(2021, 12, 5);
            var date2 = new DateTime(2060, 05, 01);
            Assert.True(infiniteFreeTimeLine.IsValid(date1));
            Assert.True(infiniteFreeTimeLine.IsValid(date2));
            infiniteFreeTimeLine.MakeItFinite(new DateTime(2050,04,2));
            Assert.True(infiniteFreeTimeLine.IsValid(date1));
            Assert.False(infiniteFreeTimeLine.IsValid(date2));
        }

        [Fact]
        public void FinitePaidTimeline_Is_Valid_If_Paid_And_Unexpired()
        {
            var expiration = new TimelineExpiration(1, TimeIn.Months);
            var now = new DateTime(2021, 12, 18);
            var invoice = new Invoice(Guid.NewGuid().ToString(), InvoiceStatus.Paid,false);
            var finitePaidTimeline = new FinitePaidTimeLine(expiration.CreateDateTimeRangeFromExpiration(now),invoice);
            Assert.True(finitePaidTimeline.IsValid(new DateTime(2021,12,28)));
            Assert.False(finitePaidTimeline.IsValid(new DateTime(2021,01,18)));
        }
        
        [Fact]
        public void InfinitePaidTimeline_Is_Valid_If_Paid_And_Until_You_Make_It_Expired()
        {
            var start = new DateTime(2021, 12, 18);
            var invoice = new Invoice(Guid.NewGuid().ToString(), InvoiceStatus.Paid,false);
            var finitePaidTimeline = new InfinitePaidTimeLine(start,invoice);
            Assert.True(finitePaidTimeline.IsValid(new DateTime(2021,12,28)));
            Assert.False(finitePaidTimeline.IsValid(new DateTime(2021,01,18)));
        }
    }
}