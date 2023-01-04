using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public  class Subscription
    {
        public Subscription()
        {
         
            Status = SubscriptionStatus.Active;
        }
        public string Id { get; set; }

        public Offer Offer { get; set; }
        public List<Interval> Intervals { get; set; }
        public Subscriber Subscriber { get; set; }
        public SubscriptionStatus Status { get; set; }

        public void AddIntervals(IEnumerable<Interval> intervals)
        {
            Intervals.AddRange(intervals);
        }
        public Interval GetCurrentTimeLine(DateTime date)
        {
            return Intervals.FirstOrDefault(t => t.DateTimeRange.Contains(date));
        }
        public bool IsValid(DateTime date)
        {
            var currentTimeLine = GetCurrentTimeLine(date);
            if (currentTimeLine is null)
            {
                return false;
            }
            return currentTimeLine.IsValid(date);
        }

        public bool IsActive()
        {
            return Status == SubscriptionStatus.Active;
        }
    }

    public enum SubscriptionStatus
    {
        Canceled,
        Paused,
        Active
    }
}