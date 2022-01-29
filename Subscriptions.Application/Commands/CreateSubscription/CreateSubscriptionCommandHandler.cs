using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Stripe;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Services.Stripe.Interfaces;
using Subscriptions.Domain.Entities;
using Invoice = Subscriptions.Domain.Entities.Invoice;
using Subscription = Subscriptions.Domain.Entities.Subscription;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand,CreateSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IOffersRepository _offersRepository;
        private readonly IInvoicesRepository _invoicesRepository;
        private readonly ISubscribersRepository _subscribersRepository;
        private readonly IMapper _mapper;
        private readonly IPaymentIntentService _paymentIntentService;

        public CreateSubscriptionCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            ISubscriptionsRepository subscriptionsRepository,
            IOffersRepository offersRepository,
            IInvoicesRepository invoicesRepository,
            ISubscribersRepository subscribersRepository,
            IMapper mapper,
            IPaymentIntentService paymentIntentService
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
            _offersRepository = offersRepository;
            _invoicesRepository = invoicesRepository;
            _subscribersRepository = subscribersRepository;
            _mapper = mapper;
            _paymentIntentService = paymentIntentService;
        }
        public async Task<CreateSubscriptionCommandResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (await _subscribersRepository.Exist(request.AppId,request.SubscriberId))
                {
                    throw new NotFoundException("");
                }
                var offer = await _offersRepository.GetOfferByNameIncludingTimelinesDefinitions(request.AppId, request.PlanName, request.OfferName);
                if (offer is null)
                {
                    throw new NotFoundException("");
                }

                    
                var now = DateTime.Now;
                var subscription = new Subscription()
                {
                    Id = Guid.NewGuid().ToString()
                };
                var timelines = new List<TimeLine>();
                var nextTimelineStart = now;
                foreach (var timeLineDefinition in offer.TimeLineDefinitions)
                {
                    timelines.AddRange(timeLineDefinition.Build(nextTimelineStart));
                    var lastTimeline = timelines.Last();
                    if (lastTimeline.DateTimeRange.End is not null)
                    {
                        nextTimelineStart = lastTimeline.DateTimeRange.End.Value;
                    }
                }
                subscription.AddTimeLines(timelines);
                await unitOfWork.CommitWork();
                var subscriptionDto = new SubscriptionDto();
                _mapper.Map(subscription, subscriptionDto);
                return new CreateSubscriptionCommandResponse()
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