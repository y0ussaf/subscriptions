using FluentValidation;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Validators
{
    public class MonthlyFinitePaidTimeLineDefinitionValidator : AbstractValidator<MonthlyExpiredPaidTimeLineDefinition>
    {
        public MonthlyFinitePaidTimeLineDefinitionValidator()
        {
            RuleFor(x => x)
                .SetValidator(new ManyFinitePaidTimeLineDefinitionValidator());

            RuleFor(x => x.StartingDay)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(28);
        }
    }
}