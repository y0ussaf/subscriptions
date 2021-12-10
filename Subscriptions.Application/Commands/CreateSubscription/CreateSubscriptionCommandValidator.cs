﻿using FluentValidation;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.SubscriberId)
                .NotNull();
        }
    }
}