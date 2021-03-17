using System.Collections.Generic;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class Plan
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? TrialExpireAfter { get; set; }
        public TimeIn? TrialExpireAfterTimeIn { get; set; } 
        public bool? TrialRequireCreditCard { get; set; }
        public App App { get; set; }
        public List<Feature> Features { get; set; }
        public List<Offer> Offers { get; set; }
        public Offer DefaultOffer { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}