using System;
using FluentValidation;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            
        }
    }
}