using System.Collections.Generic;
using MediatR;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommand : IRequest<AddOfferToPlanCommandResponse>
    {
        public long PlanId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IntervalDefinitionDto> IntervalDefinitions { get; set; }

        public AddOfferToPlanCommand()
        {
            IntervalDefinitions = new List<IntervalDefinitionDto>();
        }


        
        
    }
}