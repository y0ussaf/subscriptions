using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class Interval
    {
        public string Id { get; set; }
        protected Interval(DateTimeRange dateTimeRange)
        {
            DateTimeRange = dateTimeRange;
        }
        public IntervalDefinition IntervalDefinition { get; set; }

        public bool IsValid(DateTime date)
        {
            throw new NotImplementedException();
        }
        public DateTimeRange DateTimeRange { get; set; }
        
        public string Conditions { get; set; }
    }


    
}