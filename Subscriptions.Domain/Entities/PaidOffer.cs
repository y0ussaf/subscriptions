using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidOffer : Offer
    {
        
        public Expiration FreeTimeLineExpiration { get; set; }
        public bool AutoCharging { get; set; }
        public bool AllPaidCyclesShouldBePaid { get; set; }

        public PaidOffer(string id, Plan plan,Expiration freeCycleExpiration = null) : base(id, plan)
        {
            AutoCharging = true;
            AllPaidCyclesShouldBePaid = false;
        }
    }
}