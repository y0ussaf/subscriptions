using System.Collections.Generic;

namespace Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions
{
    public class GetIntervalsDefinitionsQueryResponse
    {
        public List<IntervalDefinitionDto> Definitions { get; set; } = new();
    }
}