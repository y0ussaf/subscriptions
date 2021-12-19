using System;
using System.Collections.Generic;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Domain.Common
{
    public class TimelineExpiration : ValueObject
    {
        public int ExpireAfter { get; private set; }
        public TimeIn ExpireAfterTimeIn { get; private set; }
        public TimelineExpiration(int expireAfter, TimeIn expireAfterTimeIn)
        {
            ExpireAfter = expireAfter;
            ExpireAfterTimeIn = expireAfterTimeIn;
        }

        public DateTimeRange CreateDateTimeRangeFromExpiration(DateTime? start = null)
        {
            start ??= DateTime.Now;
            DateTime end;
            if (ExpireAfterTimeIn == TimeIn.Days)
            { 
                end = start.Value.AddDays(ExpireAfter);
            }
            else if (ExpireAfterTimeIn == TimeIn.Hours)
            {
                end = start.Value.AddHours(ExpireAfter);
            }
            else
            {
                end = start.Value.AddMonths(ExpireAfter);
            }
            
            return new DateTimeRange(start.Value, end);

        }

   
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExpireAfter;
            yield return ExpireAfterTimeIn;
        }
    }
}