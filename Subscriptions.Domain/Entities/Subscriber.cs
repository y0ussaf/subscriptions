using System.Collections.Generic;

namespace Subscriptions.Domain.Entities
{
    public class Subscriber
    {
       
        public string Id { get; set; }
        public string Name { get; set; }
        public App App { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }
        public PaymentMethod DefaultPaymentMethod { get; set; }
       
    }
}