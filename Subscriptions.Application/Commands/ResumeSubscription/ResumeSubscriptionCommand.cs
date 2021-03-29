using MediatR;

namespace Subscriptions.Application.Commands.ResumeSubscription
{
    public class ResumeSubscriptionCommand : IRequest<ResumeSubscriptionCommandResponse>
    {
        public string SubscriptionId { get; set; }
    }
}