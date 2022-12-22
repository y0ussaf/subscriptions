using MediatR;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public class GetPlansQuery : IRequest<GetPlansQueryResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}