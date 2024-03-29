﻿using System.Collections.Generic;
using Subscriptions.Application.Queries.Plans.GetPlans;

namespace Subscriptions.Application.Queries.Plans.GetPlan
{
    public class PlanDto
    {
        public PlanDto()
        {
            Offers = new List<OfferDto>();
            Features = new List<FeatureDto>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<OfferDto> Offers { get; set; }
        public List<FeatureDto> Features { get; set; }
    }
}