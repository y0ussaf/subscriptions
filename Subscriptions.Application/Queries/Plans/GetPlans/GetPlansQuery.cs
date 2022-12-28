using MediatR;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public class GetPlansQuery : IRequest<GetPlansQueryResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public string Status { get; set; }
    }
}