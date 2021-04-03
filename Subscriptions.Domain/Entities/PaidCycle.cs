using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidCycle : Cycle
    {
        public Invoice Invoice { get; set; }
        public PaidCycle(PaidSubscription subscription,DateTimeRange dateTimeRange,Invoice invoice) : base(subscription,dateTimeRange)
        {
            Invoice = invoice;
            Type = CycleType.Paid;
        }
            
        public override bool IsValid()
        {
            return base.IsValid() && Invoice.Status == InvoiceStatus.Paid;
        }
        
    }
}