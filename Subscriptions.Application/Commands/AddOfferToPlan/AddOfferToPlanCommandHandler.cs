using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommandHandler : IRequestHandler<AddOfferToPlanCommand,AddOfferToPlanCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IOffersRepository _offersRepository;
        private readonly IPlansRepository _plansRepository;

        public AddOfferToPlanCommandHandler(IMapper mapper
            , IUnitOfWorkContext unitOfWorkContext
            ,IOffersRepository offersRepository
            ,IPlansRepository plansRepository
            )
        {
            _mapper = mapper;
            _unitOfWorkContext = unitOfWorkContext;
            _offersRepository = offersRepository;
            _plansRepository = plansRepository;
        }

        public async Task<AddOfferToPlanCommandResponse> Handle(AddOfferToPlanCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var plan = await _plansRepository.GetPlan(request.PlanId);
                    if (plan is null)
                    {
                        throw new NotFoundException("");
                    }

                    var offer = new Offer
                    {
                        Id = Guid.NewGuid().ToString(),
                        Plan = plan
                    };
                    _mapper.Map(request,offer);
                    await _plansRepository.StorePlan(plan);
                    await unitOfWork.CommitWork();
                    return new AddOfferToPlanCommandResponse()
                    {
                        Id = plan.Id
                    };
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