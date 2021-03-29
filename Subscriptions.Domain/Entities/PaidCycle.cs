using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public class PaidCycle : ExpiredCycle
    {
        public Invoice Invoice { get; set; }
        public PaidCycle(Subscription subscription,DateTimeRange dateTimeRange,Invoice invoice) : base(subscription,dateTimeRange)
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