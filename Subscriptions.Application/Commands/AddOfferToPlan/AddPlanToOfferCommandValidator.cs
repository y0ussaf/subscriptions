using System;
using FluentValidation;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddPlanToOfferCommandValidator : AbstractValidator<AddOfferToPlanCommand>
    {
        public AddPlanToOfferCommandValidator()
        {
            RuleFor(x => x.ExpireAfter)
                .NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.ExpireAfter.Value)
                        .GreaterThanOrEqualTo((uint) 1);
                });
            RuleFor(x => x.ExpireAfterTimeIn)
                .NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.ExpireAfterTimeIn)
                        .Matches($"({string.Join('|', Enum.GetNames(typeof(TimeIn)))})");
                });
            RuleFor(x => x.Price)
                .NotNull().DependentRules(() =>
                {
                    RuleFor(x => x.Price)
                        .GreaterThanOrEqualTo(0);
                });
        }
    }
}