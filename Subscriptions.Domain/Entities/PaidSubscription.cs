using System;
using System.Collections.Generic;
using System.Linq;

namespace Subscriptions.Domain.Entities
{
    public class PaidSubscription : Subscription
    {
        public PaidOffer PaidOffer { get; set; }
        public List<Cycle> Cycles { get; set; }
        public bool AllPaidCyclesShouldBePaid { get; set; }
        public bool CreatingNextPaidCycleAutomatically { get; set; }

        public PaidSubscription(string id, Subscriber subscriber, PaidOffer paidOffer) : base(id, subscriber)
        {
            PaidOffer = paidOffer;
            SubscriptionType = SubscriptionType.Paid;
            Cycles = new List<Cycle>();
            AllPaidCyclesShouldBePaid = paidOffer.AllPaidCyclesShouldBePaid;
        }

        public override bool IsValid()
        {
            var anyCycleValidNow = Cycles.Any(c => c.IsValid());
            if (AllPaidCyclesShouldBePaid)
            {
                // all paid cycles should be paid 
                return anyCycleValidNow && Cycles.OfType<PaidCycle>().All(c => c.Invoice.Status == InvoiceStatus.Paid);
            }
            return anyCycleValidNow;
        }

        public void AddCycle(Cycle cycle)
        {
            if (Cycles.Count != 0)
            {
                var last = Cycles.Last();
                if (last.DateTimeRange.End is null )
                {
                    throw new Exception("close it first");
                    
                }

                if (last.DateTimeRange.End > cycle.DateTimeRange.Start)
                {
                    throw new Exception("");
                }
            }
            Cycles.Add(cycle);
        }
    }
}