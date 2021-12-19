using FluentValidation;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Common.Validators
{
    public class ManyFinitePaidTimeLineDefinitionValidator : AbstractValidator<ManyExpiredPaidTimeLineDefinition>
    {
        public ManyFinitePaidTimeLineDefinitionValidator()
        {
            // validate the paidTimeLineDefinition Part
            RuleFor(x => x as PaidTimeLineDefinition)
                .SetValidator(new PaidTimelineDefinitionValidator());
            
            
            RuleFor(x => x.Expiration)
                .SetValidator(new ExpirationValidator());
            RuleFor(x => x.Repeat)
                .GreaterThan(1);

        }
    }
}