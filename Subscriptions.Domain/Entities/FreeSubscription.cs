namespace Subscriptions.Domain.Entities
{
    public class FreeSubscription : Subscription
    {
        public FreeOffer FreeOffer { get; set; }

        public FreeSubscription(string id, Subscriber subscriber, FreeOffer freeOffer) : base(id, subscriber)
        {
            FreeOffer = freeOffer;
            SubscriptionType = SubscriptionType.Free;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}