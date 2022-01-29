using MediatR;

namespace Subscriptions.Application.Commands.AddPlan
{
    public class CreatePlanCommand : IRequest<CreatePlanCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public long? AppId { get; set; }
    }
    
}