using FluentValidation;

namespace Subscriptions.Application.Commands.CreateTrialSubscription
{
    public class CreateTrialSubscriptionCommandValidator : AbstractValidator<CreateTrialSubscriptionCommand>
    {
        public CreateTrialSubscriptionCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.SubscriberId)
                .NotNull();
        }
    }
}