using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreatePlan
{
    public class CreatePlanCommandHandler : IRequestHandler<CreatePlanCommand,CreatePlanCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IPlansRepository _plansRepository;
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IAppsRepository _appsRepository;

        public CreatePlanCommandHandler(IMapper mapper,
            IPlansRepository plansRepository,
            IUnitOfWorkContext unitOfWorkContext,
            IAppsRepository appsRepository
            )
        {
            _mapper = mapper;
            _plansRepository = plansRepository;
            _unitOfWorkContext = unitOfWorkContext;
            _appsRepository = appsRepository;
        }

        public async Task<CreatePlanCommandResponse> Handle(CreatePlanCommand request, CancellationToken cancellationToken)
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

                var plan = new Plan();
                _mapper.Map(request, plan);
                await _plansRepository.StorePlan(request.AppId.Value,plan);
                await unitOfWork.CommitWork();
                return new CreatePlanCommandResponse();
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}