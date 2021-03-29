using System;
using System.Collections.Generic;

namespace Subscriptions.Domain.Common
{
    public class Expiration : ValueObject
    {
        public Expiration(uint expireAfter, TimeIn expireAfterTimeIn)
        {
            ExpireAfter = expireAfter;
            ExpireAfterTimeIn = expireAfterTimeIn;
        }

        public DateTimeRange CreateDateTimeRangeFromExpiration(DateTime? start = null)
        {
            start ??= DateTime.Now;
            if (ExpireAfterTimeIn == TimeIn.Days)
            { 
                var end = start.Value.AddDays(ExpireAfter);
                return new DateTimeRange(start.Value, end);
            }
            else
            {
                var end = start.Value.AddMonths((int) ExpireAfter);
                return new DateTimeRange(start.Value, end);
            }
        }
        public uint ExpireAfter { get; private set; }
        public TimeIn ExpireAfterTimeIn { get; private set; }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ExpireAfter;
            yield return ExpireAfterTimeIn;
        }
    }
}