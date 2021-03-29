
namespace Subscriptions.Domain.Entities
{
    public class Offer
    {
        public string Id { get; set; }
        public string Name { get; set; }
      
        public OfferType OfferType { get; set; }
        public Plan Plan { get; set; }
        
    }
    public enum OfferType
    {
        Trial,
        Paid,
        Free
    }
    
}