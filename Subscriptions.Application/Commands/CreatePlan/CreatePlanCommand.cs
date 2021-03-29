using MediatR;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.CreatePlan
{
    public class CreatePlanCommand : IRequest<CreatePlanCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AppId { get; set; }
    }
    
}