using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class IntervalDefinitionDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int Order { get; set; }

    }
}