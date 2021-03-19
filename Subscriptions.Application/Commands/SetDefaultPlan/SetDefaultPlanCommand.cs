using MediatR;

namespace Subscriptions.Application.Commands.SetDefaultPlan
{
    public class SetDefaultPlanCommand : IRequest
    {
        public string PlanId { get; set; }
        public string AppId { get; set; }
    }
}