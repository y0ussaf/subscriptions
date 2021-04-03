namespace Subscriptions.Domain.Entities
{
    public class FreeOffer : Offer
    {
        public FreeOffer(string id, Plan plan) : base(id, plan)
        {
        }
    }
}