using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidOfferWithSpecificDayOnWitchCyclesStart : PaidOffer
    {
        
        public int SpecificDayOnWitchCyclesStart { get; set; }
        
        public PaidOfferWithSpecificDayOnWitchCyclesStart(string id, Plan plan, long price, Expiration expiration, int specificDayOnWitchCyclesStart, Expiration freeCycleExpiration = null) : base(id, plan, price, expiration, freeCycleExpiration)
        {
            OfferType = OfferType.PaidOfferWithSpecificDayOnWitchCyclesStart;
            if (expiration.ExpireAfterTimeIn is not TimeIn.Months)
            {
                throw new Exception("timeIn supported for this type of offer is months");
            }
            SpecificDayOnWitchCyclesStart = specificDayOnWitchCyclesStart;
        }

        public override PaidCycle CreatePaidCycle(PaidSubscription subscription, Invoice invoice, DateTime now)
        {
            var start = GetCurrentCycleStartingDateTime(now);
            return new PaidCycle(subscription, Expiration.CreateDateTimeRangeFromExpiration(start), invoice);
        }

        public override ExpiredFreeCycle CreateOfferedFreeCycle(PaidSubscription subscription, DateTime now)
        {
            var start = GetCurrentCycleStartingDateTime(now);
            return new(subscription, FreeCycleExpiration.CreateDateTimeRangeFromExpiration(start));
        }

        public override ExpiredFreeCycle CreateFreeExpiredCycle(PaidSubscription subscription, Expiration expiration, DateTime now)
        {
            var start = GetCurrentCycleStartingDateTime(now);
            return new(subscription, expiration.CreateDateTimeRangeFromExpiration(start));
        }

        public override ExpiredFreeCycle CreateExpiredFreeCycleWithUndefinedEnd(PaidSubscription subscription, DateTime now)
        {
            var start = GetCurrentCycleStartingDateTime(now);
            return base.CreateExpiredFreeCycleWithUndefinedEnd(subscription, start);
        }

        public DateTime GetCurrentCycleStartingDateTime(DateTime now)
        {
            int startMonth;
            int startYear;
            if (now.Day < SpecificDayOnWitchCyclesStart)
            {
                startMonth = now.Month == 1 ? 12 : now.Month -1;
                startYear = startMonth == 12 ? now.Year - 1 : now.Year;
            }
            else
            {
                startMonth = now.Month;
                startYear = now.Year;
            }
            return new DateTime(startYear, startMonth, SpecificDayOnWitchCyclesStart);
        }

        public DateTime GetCurrentCycleEndingDateTime(DateTime now)
        {
            int month;
            int year;
            if (now.Month == 12 && now.Day > SpecificDayOnWitchCyclesStart)
            {
                month = 1;
                year = now.Year + 1;
            }
            else
            {
                month = now.Month+1;
                year = now.Year;
            }

            return new DateTime(year, month,SpecificDayOnWitchCyclesStart);
        }
    }
}