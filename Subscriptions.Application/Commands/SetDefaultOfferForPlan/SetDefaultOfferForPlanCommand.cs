using MediatR;

namespace Subscriptions.Application.Commands.SetDefaultOfferForPlan
{
    public class SetDefaultOfferForPlanCommand : IRequest
    {
        public string PlanId { get; set; } 
        public string OfferId { get; set; }
    }
}