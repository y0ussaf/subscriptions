using System;
using FluentValidation;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.OfferPaidSubscriptionForFree
{
    public class OfferPaidSubscriptionForFreeCommandValidator : AbstractValidator<OfferPaidSubscriptionForFreeCommand>
    {
        public OfferPaidSubscriptionForFreeCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.SubscriberId)
                .NotNull();
            When(x => !x.Unlimited, () =>
            {
                RuleFor(x => x.ExpireAfter)
                    .GreaterThan(0);
                RuleFor(x => x.ExpireAfterTimeIn)
                    .NotNull().DependentRules(() =>
                    {
                        RuleFor(x => x.ExpireAfterTimeIn)
                            .Matches($"({string.Join('|', Enum.GetNames(typeof(TimeIn)))})");
                    });
            });
        }
    }
}