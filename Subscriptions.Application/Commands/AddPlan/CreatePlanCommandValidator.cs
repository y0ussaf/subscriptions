﻿using FluentValidation;

namespace Subscriptions.Application.Commands.AddPlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(x => x.AppId)
                .NotNull();
            RuleFor(x => x.Name)
                .NotEmpty();

        }
    }
}