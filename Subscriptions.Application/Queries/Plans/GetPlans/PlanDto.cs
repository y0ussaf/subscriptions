using System;
using System.Collections.Generic;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public class PlanDto
    {
        public PlanDto()
        {
         
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long TotalSubscriptions { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}