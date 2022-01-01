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
            if (!request.AppId.HasValue)
            {
                throw new Exception();
            }

            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (await _plansRepository.Exist(request.AppId.Value,request.PlanName))
                {
                    throw new NotFoundException("");
                }

                var offer = new Offer();
                _mapper.Map(request,offer);
                await _offersRepository.AddOfferToPlan(request.AppId.Value,request.PlanName,offer);
                await unitOfWork.CommitWork();
                return new AddOfferToPlanCommandResponse();
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}