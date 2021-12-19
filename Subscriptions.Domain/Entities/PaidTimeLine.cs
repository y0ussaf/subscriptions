using System;
using Subscriptions.Domain.Common;

namespace Subscriptions.Domain.Entities
{
    public abstract class PaidTimeLine : TimeLine
    {
        public Invoice Invoice { get; set; }
        public decimal Price { get; set; }
        public bool AutoCharging { get; set; }
        public PaidTimeLine(DateTimeRange dateTimeRange,Invoice invoice) : base(dateTimeRange)
        {
            Invoice = invoice;
        }

        public override bool IsValid(DateTime date)
        {
            return DateTimeRange.Contains(date) && Invoice.Status == InvoiceStatus.Paid;
        }
    }
}