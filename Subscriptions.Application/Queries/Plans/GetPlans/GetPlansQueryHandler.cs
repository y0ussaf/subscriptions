using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Plans.GetPlans.Persistence;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{
    public class GetPlansQueryHandler  : IRequestHandler<GetPlansQuery,GetPlansQueryResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IGetPlansQueryPersistence _getPlansQueryPersistence;

        public GetPlansQueryHandler(IUnitOfWorkContext unitOfWorkContext,IGetPlansQueryPersistence getPlansQueryPersistence)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _getPlansQueryPersistence = getPlansQueryPersistence;
        }

        public async Task<GetPlansQueryResponse> Handle(GetPlansQuery query, CancellationToken cancellationToken)
        {
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            query.OrderBy ??= nameof(Plan.CreatedAt);
            var (plans,count) = await _getPlansQueryPersistence.GetPlansWithCount(query);

            return new GetPlansQueryResponse()
            {
                Count = count,
                Plans = plans
            };

        }
    }
}