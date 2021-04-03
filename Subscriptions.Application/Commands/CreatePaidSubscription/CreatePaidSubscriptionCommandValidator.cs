using FluentValidation;

namespace Subscriptions.Application.Commands.CreatePaidSubscription
{
    public class CreatePaidSubscriptionCommandValidator : AbstractValidator<CreatePaidSubscriptionCommand>
    {
        public CreatePaidSubscriptionCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.SubscriberId)
                .NotNull();
        }
    }
}