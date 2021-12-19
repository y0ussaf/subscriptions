﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public  class Subscription
    {
        public Subscription(string id, Subscriber subscriber,Offer offer)
        {
            Id = id;
            Subscriber = subscriber;
            Offer = offer;
            Status = SubscriptionStatus.Active;
        }
        public Offer Offer { get; set; }
        private List<TimeLine> TimeLines { get; set; }
        public string Id { get; set; }
        public Subscriber Subscriber { get; set; }
        public SubscriptionStatus Status { get; set; }

        public void AddTimeLines(IEnumerable<TimeLine> timeLines)
        {
            TimeLines.AddRange(timeLines);
        }
        public TimeLine GetCurrentTimeLine(DateTime date)
        {
            return TimeLines.FirstOrDefault(t => t.DateTimeRange.Contains(date));
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