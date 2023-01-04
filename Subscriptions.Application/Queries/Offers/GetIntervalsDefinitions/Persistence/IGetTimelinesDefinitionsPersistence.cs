using System.Collections.Generic;
using System.Threading.Tasks;

namespace Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions.Persistence
{
    public interface IGetTimelinesDefinitionsPersistence
    {
        Task<List<IntervalDefinitionDto>> GetIntervalsDefinitions(long offerId);
    }
}