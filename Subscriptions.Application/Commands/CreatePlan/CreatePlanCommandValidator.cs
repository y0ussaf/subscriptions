using System;
using FluentValidation;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            When(x => x.TrialExpireAfter.HasValue, () =>
            {
                RuleFor(x => x.TrialExpireAfterTimeIn)
                    .NotNull().DependentRules(() =>
                    {
                        RuleFor(x => x.TrialExpireAfterTimeIn)
                            .Matches($"({string.Join('|', Enum.GetNames(typeof(TimeIn)))})");
                    });

            });

        }
    }
}