﻿
using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class Offer
    {
        
        public Offer(string id, Plan plan)
        {
            Id = id;
            Plan = plan;
            TimeLineDescriptions = new List<TimeLineDefinition>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public Plan Plan { get; set; }
        public ICollection<TimeLineDefinition> TimeLineDescriptions { get; set; }
    }
   
    
}