using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Commands.AddFeatureToPlan.Persistence;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Persistence;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanCommandHandler : IRequestHandler<AddFeatureToPlanCommand,Unit>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IAddFeatureToPlanCommandPersistence _addFeatureToPlanCommandPersistence;
        private readonly IPlansPersistence _plansPersistence;
 

        public AddFeatureToPlanCommandHandler(IMapper mapper,IUnitOfWorkContext unitOfWorkContext
            ,IAddFeatureToPlanCommandPersistence addFeatureToPlanCommandPersistence,IPlansPersistence plansPersistence)
        {
            _mapper = mapper;
            _unitOfWorkContext = unitOfWorkContext;
            _addFeatureToPlanCommandPersistence = addFeatureToPlanCommandPersistence;
            _plansPersistence = plansPersistence;
        }

        public async Task<Unit> Handle(AddFeatureToPlanCommand request, CancellationToken cancellationToken)
        {

            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (await _plansPersistence.PlanExist(request.PlanId))
                {
                    throw new NotFoundException(string.Empty);
                }
                var planFeature = new PlanFeature()
                {
                    Plan = new Plan {Id = request.PlanId},
                    Feature = new Feature{Id = request.FeatureId},
                    Details = request.Details
                };
                await _addFeatureToPlanCommandPersistence.AddFeatureToPlan(planFeature);
                await unitOfWork.CommitWork();
                return Unit.Value;
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}