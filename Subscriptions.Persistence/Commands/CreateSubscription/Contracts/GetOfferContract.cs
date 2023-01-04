using NodaTime;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.CreateSubscription.Contracts
{
    public class GetOfferContract
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }
    }
}