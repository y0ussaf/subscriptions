using FluentValidation;

namespace Subscriptions.Application.Commands.UnblockSubscription
{
    public class UnblockSubscriptionCommandValidator : AbstractValidator<UnblockSubscriptionCommand>
    {
        public UnblockSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotNull();
        }
    }
}