using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Subscriptions.Application.Queries.Offers.GetOffer
{
    public class GetOfferQueryHandler : IRequestHandler<GetOfferQuery,GetOfferQueryResponse>
    {
        public Task<GetOfferQueryResponse> Handle(GetOfferQuery request, CancellationToken cancellationToken)
        {
            
        }
    }
}