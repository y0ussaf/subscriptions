using FluentValidation;

namespace Subscriptions.Application.Commands.RegisterBackendApp
{
    public class RegisterBackendAppCommandValidator : AbstractValidator<RegisterBackendAppCommand>
    {
        public RegisterBackendAppCommandValidator()
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