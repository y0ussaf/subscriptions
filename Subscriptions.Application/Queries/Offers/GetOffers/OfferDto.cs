using System;

namespace Subscriptions.Application.Queries.Offers.GetOffers
{
    public class OfferDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long TotalSubscriptions { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}