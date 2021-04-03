using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateFreeSubscription
{
    public class CreateFreeSubscriptionCommandHandler : IRequestHandler<CreateFreeSubscriptionCommand,CreateFreeSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;
        private readonly ISubscribersRepository _subscribersRepository;
        private readonly IMapper _mapper;

        public CreateFreeSubscriptionCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            ISubscriptionsRepository subscriptionsRepository,
            IOffersRepository offersRepository,
            ISubscribersRepository subscribersRepository,
            IMapper mapper
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
            _offersRepository = offersRepository;
            _subscribersRepository = subscribersRepository;
            _mapper = mapper;
        }
        public async Task<CreateFreeSubscriptionCommandResponse> Handle(CreateFreeSubscriptionCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var offer = await _offersRepository.GetOffer(request.OfferId);
                    var subscriber = await _subscribersRepository.GetSubscriber(request.SubscriberId);
                    if (offer is null)
                    {
                        throw new NotFoundException("");
                    }

                    if (subscriber is null)
                    {
                        throw new NotFoundException("");
                    }

                    var subscriptionId = Guid.NewGuid().ToString();
                    if (!(offer is FreeOffer))
                    {
                        throw new BadRequestException("");
                    }

                    var freeOffer = offer as FreeOffer;
                    var subscription = new FreeSubscription(subscriptionId, subscriber, freeOffer);
                    await _subscriptionsRepository.StoreSubscription(subscription);
                    await unitOfWork.CommitWork();
                    var subscriptionDto = new SubscriptionDto();
                    _mapper.Map(subscription, subscriptionDto);
                    return new CreateFreeSubscriptionCommandResponse()
                    {
                        Subscription = subscriptionDto
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