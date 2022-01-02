using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Commands.SetDefaultPlan
{
    public class SetDefaultPlanCommandHandler : IRequestHandler<SetDefaultPlanCommand>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IPlansRepository _plansRepository;
        private readonly IAppsRepository _appsRepository;

        public SetDefaultPlanCommandHandler(IUnitOfWorkContext unitOfWorkContext,
            IPlansRepository plansRepository,
            IAppsRepository appsRepository
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _plansRepository = plansRepository;
            _appsRepository = appsRepository;
        }

        public async Task<Unit> Handle(SetDefaultPlanCommand request, CancellationToken cancellationToken)
        {
            if (!request.AppId.HasValue)
            {
                throw new InvalidOperationException();
            }

            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (await _appsRepository.Exist(request.AppId.Value))
                {
                    throw new NotFoundException("");
                }

                if (await _plansRepository.Exist(request.AppId.Value,request.PlanName))
                {
                    throw new NotFoundException("");
                }
                await _appsRepository.SetDefaultPlan(request.AppId.Value,request.PlanName);
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