using FluentValidation;

namespace Subscriptions.Application.Commands.RegisterFrontendApp
{
    public class RegisterFrontendAppCommandValidator : AbstractValidator<RegisterFrontendAppCommand>
    {
        public RegisterFrontendAppCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.Name)
                        .MinimumLength(3);
                });
        }
    }
}