using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class TrialSubscription : Subscription
    {
        public TrialOffer TrialOffer { get; set; }
        public DateTimeRange DateTimeRange { get; set; }
        public TrialSubscription(string id, Subscriber subscriber, TrialOffer trialOffer, DateTimeRange dateTimeRange) : base(id, subscriber)
        {
            TrialOffer = trialOffer;
            DateTimeRange = dateTimeRange;
            SubscriptionType = SubscriptionType.Trial;
        }

        public override bool IsValid()
        {
            return DateTimeRange.Contains(DateTime.Now);
        }
    }
}