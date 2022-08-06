using System.Collections.Generic;
using MediatR;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommand : IRequest<AddOfferToPlanCommandResponse>
    {
        public AddOfferToPlanCommand()
        {
            TimeLineDefinitions = new List<TimelineDefinitionDto>();
        }

        public string Name { get; set; }
        public long PlanId { get; set; }
        public IEnumerable<TimelineDefinitionDto> TimeLineDefinitions { get; set; }
        
        
    }
}