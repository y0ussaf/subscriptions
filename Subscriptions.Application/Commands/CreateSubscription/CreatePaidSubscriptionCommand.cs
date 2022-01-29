﻿using MediatR;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<CreateSubscriptionCommandResponse>
    {
        public string AppId { get; set; }
        public string PlanName { get; set; }
        public string OfferName { get; set; }
        public string SubscriberId { get; set; }
    }
}