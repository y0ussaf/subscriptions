using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.OfferPaidSubscriptionForFree
{
    public class OfferPaidSubscriptionForFreeCommandHandler : IRequestHandler<OfferPaidSubscriptionForFreeCommand,OfferPaidSubscriptionForFreeCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscribersRepository _subscribersRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;

        public OfferPaidSubscriptionForFreeCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            ISubscribersRepository subscribersRepository,
            ISubscriptionsRepository subscriptionsRepository,
            IOffersRepository offersRepository
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscribersRepository = subscribersRepository;
            _subscriptionsRepository = subscriptionsRepository;
            _offersRepository = offersRepository;
        }

        public async Task<OfferPaidSubscriptionForFreeCommandResponse> Handle(OfferPaidSubscriptionForFreeCommand request, CancellationToken cancellationToken)
        {
            // app owner allowed
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var subscriber = await _subscribersRepository.GetSubscriber(request.SubscriberId);
                    if (subscriber is null)
                    {
                        throw new NotFoundException("");
                    }

                    var offer = await _offersRepository.GetOffer(request.OfferId);
                    if (offer is null)
                    {
                        throw new NotFoundException("");
                    }

                    if (offer is not  PaidOffer )
                    {
                        throw new BadRequestException("");
                    }

                    var paidOffer = offer as PaidOffer;
                    var subscriptionId = Guid.NewGuid().ToString();
                    var paidSubscription = new PaidSubscription(subscriptionId, subscriber, paidOffer)
                    {
                        CreatingNextPaidCycleAutomatically = request.CreatingNextPaidCycleAutomatically
                    };
                    var now = DateTime.Now;
                    Cycle cycle;
                    if (request.Unlimited)
                    {
                        cycle = paidOffer.CreateExpiredFreeCycleWithUndefinedEnd(paidSubscription,now);
                    }
                    else
                    {
                        Enum.TryParse(request.ExpireAfterTimeIn, out TimeIn timeIn);
                        var expiration = new Expiration(request.ExpireAfter, timeIn);
                        cycle = paidOffer.CreateFreeExpiredCycle(paidSubscription,expiration,now);
                    }

                    await _subscriptionsRepository.StoreSubscription(paidSubscription);
                    await _subscriptionsRepository.AddCycle(cycle);
                    await unitOfWork.CommitWork();
                    return new OfferPaidSubscriptionForFreeCommandResponse();
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