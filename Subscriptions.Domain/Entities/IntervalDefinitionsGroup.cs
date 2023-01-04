using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class IntervalDefinitionsGroup
    {
        public string Name { get; set; }
        public IntervalDefinitionsGroup()
        {
            IntervalDefinitions = new List<IntervalDefinition>();
        }

        public List<IntervalDefinition> IntervalDefinitions { get; set; }
    }
}