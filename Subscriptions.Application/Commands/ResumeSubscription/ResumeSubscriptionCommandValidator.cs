using FluentValidation;

namespace Subscriptions.Application.Commands.ResumeSubscription
{
    public class ResumeSubscriptionCommandValidator : AbstractValidator<ResumeSubscriptionCommand>
    {
        public ResumeSubscriptionCommandValidator()
        {
            RuleFor(x => x.SubscriptionId)
                .NotNull();
        }
    }
}