using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Offers.GetOffers.Persistence;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Queries.Offers.GetOffers
{
    public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery,GetOffersQueryResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IGetOffersQueryPersistence _getOffersQueryPersistence;
        
        public GetOffersQueryHandler(IUnitOfWorkContext unitOfWorkContext,IGetOffersQueryPersistence getOffersQueryPersistence)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _getOffersQueryPersistence = getOffersQueryPersistence;
        }

        public async Task<GetOffersQueryResponse> Handle(GetOffersQuery query, CancellationToken cancellationToken)
        {
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            query.OrderBy ??= nameof(Offer.CreatedAt);

            var (offers,count) = await _getOffersQueryPersistence.GetOffersWithCount(query);
            return new GetOffersQueryResponse()
            {
                Offers = offers,
                Count = count
            };
        }
    }
}