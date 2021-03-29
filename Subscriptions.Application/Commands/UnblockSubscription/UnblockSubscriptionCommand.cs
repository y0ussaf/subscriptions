using MediatR;

namespace Subscriptions.Application.Commands.UnblockSubscription
{
    public class UnblockSubscriptionCommand  : IRequest<UnblockSubscriptionCommandResponse>
    {
        public string SubscriptionId { get; set; }
    }
}