using System.Collections.Generic;
using MediatR;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddOfferToPlan
{
    public class AddOfferToPlanCommand : IRequest<AddOfferToPlanCommandResponse>
    {
        public AddOfferToPlanCommand()
        {
            TimeLineDefinitions = new List<TimeLineDefinition>();
        }

        public string Name { get; set; }
        public long? AppId { get; set; }
        public string PlanName { get; set; }
        public TimelineExpiration Expiration { get; set; }
        public IEnumerable<TimeLineDefinition> TimeLineDefinitions { get; set; }
    }
}