using System;
using System.Collections;
using System.Collections.Generic;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;
using Xunit;

namespace Subscriptions.Domain.Tests.Domain
{
    public class PaidOfferWithSpecificDayOnWitchCyclesStartTests
    {
        
        [Theory]
        [ClassData(typeof(GetCurrentCycleEndingDateTimeTestData))]
        public void Get_Current_Cycle_Ending_DateTime(DateTime now,int dayOnWitchCyclesStart,DateTime expected)
        {
            var offer = CreateOffer(dayOnWitchCyclesStart);
            var currentCycleEndingDateTime = offer.GetCurrentCycleEndingDateTime(now);
            Assert.Equal(expected,currentCycleEndingDateTime);
        }

        [Theory]
        [ClassData(typeof(GetCurrentCycleStartingDateTimeTestData))]
        public void Get_Current_Cycle_Starting_DateTime(DateTime now,int dayOnWitchCyclesStart,DateTime expected)
        {
            var offer = CreateOffer(dayOnWitchCyclesStart);
            var currentCycleEndingDateTime = offer.GetCurrentCycleStartingDateTime(now);
            Assert.Equal(expected,currentCycleEndingDateTime);
        }
    
        

        public PaidOfferWithSpecificDayOnWitchCyclesStart CreateOffer(int dayOnWitchCyclesStart)
        {
            var id = Guid.NewGuid().ToString();
            var plan = new Plan();
            var expiration = new Expiration(1, TimeIn.Months);
            var price = 12;
            return  new PaidOfferWithSpecificDayOnWitchCyclesStart(id, plan, price, expiration, dayOnWitchCyclesStart);
        }
        class GetCurrentCycleEndingDateTimeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var data = new List<object[]>
                    {
                        new object []{new DateTime(2020,12,19),1,new DateTime(2021, 1, 1)},
                        new object []{new DateTime(2020,12,29),15,new DateTime(2021,1,15)}
                    };
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class GetCurrentCycleStartingDateTimeTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var data = new List<object[]>
                {
                    new object []{new DateTime(2020,12,19),1,new DateTime(2020, 12, 1)},
                    new object []{new DateTime(2021,1,10),15,new DateTime(2020,12,15)}
                };
                return data.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}