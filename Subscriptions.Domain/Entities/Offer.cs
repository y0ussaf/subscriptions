
using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class Offer
    {
        
        public Offer(Plan plan)
        {
            Plan = plan;
            TimeLineDefinitions = new List<TimeLineDefinition>();
        }
        public string Name { get; set; }
        public Plan Plan { get; set; }
        public ICollection<TimeLineDefinition> TimeLineDefinitions { get; set; }
    }
   
    
}