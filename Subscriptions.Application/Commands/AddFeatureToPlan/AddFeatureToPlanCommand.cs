using MediatR;

namespace Subscriptions.Application.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanCommand : IRequest<AddFeatureToPlanResponse>
    {
        public string PlanId { get; set; }
        public string Description { get; set; }
    }
}