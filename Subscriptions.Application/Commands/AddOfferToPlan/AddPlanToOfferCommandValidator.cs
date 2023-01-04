using System;
using FluentValidation;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddPlanToOfferCommandValidator : AbstractValidator<AddOfferToPlanCommand>
    {
        public AddPlanToOfferCommandValidator()
        {
                
          
        }
    }

  
 }