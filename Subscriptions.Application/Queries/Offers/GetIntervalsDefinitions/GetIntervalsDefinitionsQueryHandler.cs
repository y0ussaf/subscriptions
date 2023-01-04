using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions.Persistence;

namespace Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions
{
    public class GetIntervalsDefinitionsQueryHandler : IRequestHandler<GetIntervalsDefinitionsQuery,GetIntervalsDefinitionsQueryResponse>
    {
        private readonly IGetTimelinesDefinitionsPersistence _getTimelinesDefinitionsPersistence;
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetIntervalsDefinitionsQueryHandler(IGetTimelinesDefinitionsPersistence getTimelinesDefinitionsPersistence,
            IUnitOfWorkContext unitOfWorkContext)
        {
            _getTimelinesDefinitionsPersistence = getTimelinesDefinitionsPersistence;
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<GetIntervalsDefinitionsQueryResponse> Handle(GetIntervalsDefinitionsQuery request, CancellationToken cancellationToken)
        {
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                var definitions = await _getTimelinesDefinitionsPersistence.GetIntervalsDefinitions(request.OfferId);
                return new GetIntervalsDefinitionsQueryResponse()
                {
                    Definitions = definitions
                };
            }catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}