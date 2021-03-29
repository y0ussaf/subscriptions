using FluentValidation;

namespace Subscriptions.Application.Commands.BlockSubscription
{
    public class BlockSubscriptionCommandValidator : AbstractValidator<BlockSubscriptionCommand>
    {
        public BlockSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotNull();
        }
    }
}