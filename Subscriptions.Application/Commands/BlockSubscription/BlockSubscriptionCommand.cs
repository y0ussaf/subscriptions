using MediatR;

namespace Subscriptions.Application.Commands.BlockSubscription
{
    public class BlockSubscriptionCommand : IRequest<BlockSubscriptionCommandResponse>
    {
        public string SubscriptionId { get; set; }
    }
}