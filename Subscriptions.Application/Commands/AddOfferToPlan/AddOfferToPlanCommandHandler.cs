using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Commands.AddOfferToPlan.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommandHandler : IRequestHandler<AddOfferToPlanCommand,AddOfferToPlanCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IAddOfferToPlanCommandPersistence _addOfferToPlanCommandPersistence;

        public AddOfferToPlanCommandHandler(IMapper mapper
            , IUnitOfWorkContext unitOfWorkContext
            ,IAddOfferToPlanCommandPersistence addOfferToPlanCommandPersistence)
        {
            _mapper = mapper;
            _unitOfWorkContext = unitOfWorkContext;
            _addOfferToPlanCommandPersistence = addOfferToPlanCommandPersistence;
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

                if (await _addOfferToPlanCommandPersistence.OfferExist(request.AppId.Value,request.PlanName,request.Name))
                {
                    throw new InvalidOperationException();
                }
                var offer = new Offer();
                _mapper.Map(request,offer);
                await _addOfferToPlanCommandPersistence.AddOffer(request.AppId.Value,request.PlanName,offer);
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