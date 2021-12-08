using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class TimeLineDescriptionsDefinition
    {
        public TimeLineDescriptionsDefinition()
        {
            TimeLineDescriptions = new List<TimeLineDescription>();
        }

        public string Name { get; set; }
        public ICollection<TimeLineDescription> TimeLineDescriptions { get; set; }
    }
}