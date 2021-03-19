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
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var app = await _appsRepository.GetApp(request.AppId);
                    if (app is null)
                    {
                        throw new NotFoundException("");
                    }

                    var plan = await _plansRepository.GetPlan(request.PlanId);
                    if (plan is null)
                    {
                        throw new NotFoundException("");
                    }
                    await _plansRepository.SetDefaultPlan(request.AppId,request.PlanId);
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
}