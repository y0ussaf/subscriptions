using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand,CreateSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;

        public CreateSubscriptionCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            ISubscriptionsRepository subscriptionsRepository,
            IOffersRepository offersRepository
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
            _offersRepository = offersRepository;
        }

        public async Task<CreateSubscriptionCommandResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var offer = await _offersRepository.GetOffer(request.OfferId);
                    if (offer is null)
                    {
                        throw new NotFoundException("");
                    }

                    var lastSubscription = await _subscriptionsRepository.LastSubscription(request.SubscriberId, offer.Plan.App.Id);
                    
                    if (lastSubscription.IsValid())
                    {
                        throw new BadRequestException("");
                    }
                    var response = new CreateSubscriptionCommandResponse();
                    switch (offer.OfferType)
                    {
                        case OfferType.Trial:
                        {
                            var trialOffer = (TrialOffer) offer;
                            var isFirstTrialSubscription =
                                await _subscriptionsRepository.IsFirstTrialSubscription(request.SubscriberId, trialOffer.Plan.App.Id);
                        
                            if (!isFirstTrialSubscription)
                            {
                                throw new BadRequestException("");
                            }
                        
                            var subscription = new TrialSubscription
                            {
                                Id = Guid.NewGuid().ToString(),
                                SubscriptionType = SubscriptionType.Trial,
                                SubscriberId = request.SubscriberId,
                                Offer = trialOffer,
                                Active = true
                            };
                            var startDate = DateTime.Now;
                            DateTime endDate;
                            if (trialOffer.Expiration.ExpireAfterTimeIn == TimeIn.Days)
                            {
                                endDate = startDate.AddDays(trialOffer.Expiration.ExpireAfter);
                            }
                            else
                            {
                                endDate = startDate.AddMonths((int) trialOffer.Expiration.ExpireAfter);
                            }

                            subscription.DateTimeRange = new DateTimeRange(startDate, endDate);
                            subscription.SubscriptionType = SubscriptionType.Trial;
                            await _subscriptionsRepository.CreateSubscription(subscription);
                            response.Id = subscription.Id;
                            break;
                        }
                        case OfferType.Paid:
                        {
                            
                            var subscription = new PaidSubscription
                            {
                                Id = Guid.NewGuid().ToString(),
                                SubscriberId = request.SubscriberId,
                                SubscriptionType = SubscriptionType.Paid,
                                Offer = offer,
                                Paid = false,
                                Active = true
                            };
                            
                            await _subscriptionsRepository.CreateSubscription(subscription);
                            response.Id = subscription.Id;
                            break;
                        }
                        case OfferType.Free:
                        {
                            var subscription = new FreeSubscription()
                            {
                                Id = Guid.NewGuid().ToString(),
                                Active = true,
                                SubscriberId = request.SubscriberId,
                                SubscriptionType = SubscriptionType.Free
                            };
                            await _subscriptionsRepository.CreateSubscription(subscription);
                            response.Id = subscription.Id;
                            break;
                        }
                    }
                    await unitOfWork.CommitWork();
                    return response;
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