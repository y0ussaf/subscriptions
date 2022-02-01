using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Commands.CreateSubscription.Persistence;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;
using Subscription = Subscriptions.Domain.Entities.Subscription;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand,CreateSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IMapper _mapper;
        private readonly ICreateSubscriptionCommandPersistence _persistence;

        public CreateSubscriptionCommandHandler(
            IUnitOfWorkContext unitOfWorkContext,
            IMapper mapper,
            ICreateSubscriptionCommandPersistence persistence
        )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _mapper = mapper;
            _persistence = persistence;
        }
        public async Task<CreateSubscriptionCommandResponse> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                if (await _persistence.SubscriberExist(request.SubscriberId))
                {
                    throw new NotFoundException("");
                }
                var offer = await _persistence.GetOffer(request.PlanName, request.OfferName);
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
                await _persistence.AddSubscription(request.PlanName,request.OfferName,subscription);
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