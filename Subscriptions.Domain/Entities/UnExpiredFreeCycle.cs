namespace Subscriptions.Domain.Entities
{
    public class UnExpiredFreeCycle : Cycle
    {

        public UnExpiredFreeCycle(Subscription subscription) : base(subscription)
        {
            Type = CycleType.ExpiredFree;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}