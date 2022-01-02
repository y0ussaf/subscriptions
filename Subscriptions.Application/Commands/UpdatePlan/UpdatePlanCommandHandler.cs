using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.UpdatePlan
{
    public class UpdatePlanCommandHandler : IRequestHandler<UpdatePlanCommand>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IAppsRepository _appsRepository;
        private readonly IPlansRepository _plansRepository;
        private readonly IMapper _mapper;

        public UpdatePlanCommandHandler(IUnitOfWorkContext unitOfWorkContext,IAppsRepository appsRepository,IPlansRepository plansRepository
        ,IMapper mapper)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _appsRepository = appsRepository;
            _plansRepository = plansRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdatePlanCommand request, CancellationToken cancellationToken)
        {
            if (!request.AppId.HasValue)
            {
                throw new InvalidOperationException();
            }

            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (!await _appsRepository.Exist(request.AppId.Value))
                {
                    throw new NotFoundException("");
                }

                var plan = await _plansRepository.GetPlanByName(request.AppId.Value,request.Name);
                if (plan is null)
                {
                    throw new NotFoundException("");
                }
                if (request.NewName != null && plan.Name != request.NewName)
                {
                    if (await _plansRepository.Exist(request.AppId.Value,request.NewName))
                    {
                        throw new InvalidOperationException("");
                    }

                    plan.Name = request.NewName;
                }
                plan.Description = request.Description;
                await _plansRepository.UpdatePlan(request.AppId.Value, request.Name, plan);
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