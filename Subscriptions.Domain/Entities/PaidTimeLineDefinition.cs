using System;

namespace Subscriptions.Domain.Entities
{
    public abstract class PaidTimeLineDefinition : TimeLineDefinition
    {
        public decimal Price { get; set; }
        public bool AutoCharging { get; set; }
    }
}