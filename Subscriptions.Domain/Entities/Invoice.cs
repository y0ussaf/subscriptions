namespace Subscriptions.Domain.Entities
{
    public class Invoice
    {
        public string Id { get; set; }
        public Payment Payment { get; set; }
        public bool AutoCharging { get; set; }
        public InvoiceStatus Status { get; set; }

        public Invoice(string id , InvoiceStatus status, bool autoCharging,Payment payment)
        {
            
            Id = id;
            Status = status;
            AutoCharging = autoCharging;
            Payment = payment;
        }
    }
    public enum InvoiceStatus
    {
        Draft,
        WaitingToBePaid,
        Paid,
        Canceled,
    }
    
}