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
    }
}