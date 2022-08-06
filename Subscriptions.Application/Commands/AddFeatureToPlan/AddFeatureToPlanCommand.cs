using MediatR;

namespace Subscriptions.Application.Commands.AddFeatureToPlan
{
    public class AddFeatureToPlanCommand : IRequest<Unit>
    {
        public long FeatureId { get; set; }
        public string Details { get; set; }
        public long PlanId { get; set; }
    }
}