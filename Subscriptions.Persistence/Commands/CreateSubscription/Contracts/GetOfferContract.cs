using Subscriptions.Domain.Entities;

namespace Subscriptions.Persistence.Commands.CreateSubscription.Contracts
{
    public class GetOfferContract
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Minutes { get; set; }
        public int Hours { get; set; }
        public int Days { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        public TimelineDefinitionType Discriminator { get; set; }
    }
}