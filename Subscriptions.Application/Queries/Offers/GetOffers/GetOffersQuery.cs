using MediatR;

namespace Subscriptions.Application.Queries.Offers.GetOffers
{
    public class GetOffersQuery : IRequest<GetOffersQueryResponse>
    {
        public long PlanId { get; set; }
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}