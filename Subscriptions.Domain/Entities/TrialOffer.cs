using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class TrialOffer : ExpiredOffer
    {
        public bool TrialRequireCreditCard { get; set; }
    }
}