using FluentValidation;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator()
        {
            RuleFor(x => x.AppId)
                .NotNull();
            RuleFor(x => x.PlanName)
                .NotNull();
            RuleFor(x => x.OfferName)
                .NotEmpty();
            RuleFor(x => x.SubscriberId)
                .NotNull();
        }
    }
}