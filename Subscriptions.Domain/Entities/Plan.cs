using System;
using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class Plan
    {
        public Plan()
        {
            Offers = new List<Offer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public PlanStatus Status { get; set; }
        public List<Offer> Offers { get; set; }
    }

    public enum PlanStatus
    {
        Active,
        Inactive,
        Archived
    }
}