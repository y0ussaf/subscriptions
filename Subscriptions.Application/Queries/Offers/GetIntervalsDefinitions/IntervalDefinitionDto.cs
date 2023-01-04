
namespace Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions
{
    public class IntervalDefinitionDto
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public int Order { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}