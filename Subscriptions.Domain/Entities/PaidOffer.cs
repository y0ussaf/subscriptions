using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidOffer : ExpiredOffer
    {
        public decimal Price { get; set; }
    }
}