namespace Subscriptions.Domain.Entities
{
    public class PlanFeature
    {
        public Plan Plan { get; set; }
        public Feature Feature { get; set; }
        public string Details { get; set; }
    }
}