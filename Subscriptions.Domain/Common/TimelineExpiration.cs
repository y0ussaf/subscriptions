using System;
using System.Collections.Generic;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Domain.Common
{
    public class TimelineExpiration : ValueObject
    {
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        public TimelineExpiration(int minutes, int hours, int days, int months, int years)
        {
            Minutes = minutes;
            Hours = hours;
            Days = days;
            Months = months;
            Years = years;
        }

        public DateTimeRange CreateDateTimeRangeFromExpiration(DateTime? start = null)
        {
            start ??= DateTime.Now;
            var end = start.Value.AddYears(Years).AddMonths(Months)
                .AddDays(Days).AddHours(Hours)
                .AddMinutes(Minutes);
            
            return new DateTimeRange(start.Value, end);

        }

   
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Minutes;
            yield return Hours;
            yield return Days;
            yield return Months;
            yield return Years;
        }
    }
}