using FluentValidation;

namespace Subscriptions.Application.Commands.CreateFreeSubscription
{
    public class CreateFreeSubscriptionCommandValidator : AbstractValidator<CreateFreeSubscriptionCommand>
    {
        public CreateFreeSubscriptionCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.SubscriberId)
                .NotNull();
        }
    }
}