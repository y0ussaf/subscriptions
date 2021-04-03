using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateTrialSubscription
{
    public class CreateTrialSubscriptionCommandHandler : IRequestHandler<CreateTrialSubscriptionCommand,CreateTrialSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;
        private readonly ISubscribersRepository _subscribersRepository;
        private readonly IMapper _mapper;

        public CreateTrialSubscriptionCommandHandler(
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

        public async Task<CreateTrialSubscriptionCommandResponse> Handle(CreateTrialSubscriptionCommand request, CancellationToken cancellationToken)
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

                    if (!(offer is TrialOffer))
                    {
                        throw new BadRequestException("");
                    }

                    var trialOffer = offer as TrialOffer;
                    if (trialOffer.TrialRequireCreditCard && subscriber.DefaultPaymentMethod is null)
                    {
                        throw new BadRequestException("this trial offer require a credit card");
                    }
                    var subscriptionId = Guid.NewGuid().ToString();
                    var dateTimeRange = trialOffer.Expiration.CreateDateTimeRangeFromExpiration();
                    var subscription = new TrialSubscription(subscriptionId, subscriber, trialOffer,dateTimeRange);
                    await _subscriptionsRepository.StoreSubscription(subscription);
                    await unitOfWork.CommitWork();

                    var subscriptionDto = new SubscriptionDto();
                    _mapper.Map(subscription, subscriptionDto);
                    return new CreateTrialSubscriptionCommandResponse()
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