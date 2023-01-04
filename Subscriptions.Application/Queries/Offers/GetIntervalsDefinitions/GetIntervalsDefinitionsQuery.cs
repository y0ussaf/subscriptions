using MediatR;

namespace Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions
{
    public class GetIntervalsDefinitionsQuery : IRequest<GetIntervalsDefinitionsQueryResponse>
    {
        public long OfferId { get; set; }
    }
}