using FluentValidation;

namespace Subscriptions.Application.Commands.SetDefaultOfferForPlan
{
    public class SetDefaultOfferForPlanCommandValidator : AbstractValidator<SetDefaultOfferForPlanCommand>
    {
        public SetDefaultOfferForPlanCommandValidator()
        {
            RuleFor(x => x.OfferId)
                .NotNull();
            RuleFor(x => x.PlanId)
                .NotNull();
        }
    }
}