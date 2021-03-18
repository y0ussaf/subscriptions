using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class Offer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public uint ExpireAfter { get; set; }
        public TimeIn ExpireAfterTimeIn { get; set; }
        public decimal Price { get; set; }
        public Plan Plan { get; set; }
    }

    
}