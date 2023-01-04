
using System;
using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class Offer
    {
        public Offer()
        {
            IntervalDefinitions = new List<IntervalDefinition>();
        }

        public Offer(Plan plan)
        {
            Plan = plan;
            IntervalDefinitions = new List<IntervalDefinition>();
        }
        public string Name { get; set; }
        public Plan Plan { get; set; }
        public List<IntervalDefinition> IntervalDefinitions { get; set; }
        public long Id { get; set; }
        public string Description { get; set; }
        public OfferStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public void AddIntervalDefinition(IntervalDefinition definition)
        {
            IntervalDefinitions.Add(definition);
        }
        public void AddIntervalDefinitions(IEnumerable<IntervalDefinition> definitions)
        {
            IntervalDefinitions.AddRange(definitions);
        }
    }

    public enum OfferStatus
    {
        Active,
        Inactive,
    }
   
    
}