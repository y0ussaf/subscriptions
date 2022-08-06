using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class TimelineDefinitionDto
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public TimelineDefinitionType TimeLineDefinitionType { get; set; }
        private ExpirationDto Expiration { get; set; }
        public int Repeat { get; set; }
        public decimal Amount { get; set; }
        public bool AutoCharging { get; set; }
    }
}