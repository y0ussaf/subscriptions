using FluentValidation;

namespace Subscriptions.Application.Commands.EndExpiredFreeCycle
{
    public class EndExpiredFreeCycleCommandValidator : AbstractValidator<EndExpiredFreeCycleCommand>
    {
        public EndExpiredFreeCycleCommandValidator()
        {
            RuleFor(x => x.CycleId)
                .NotNull();
        }
    }
}