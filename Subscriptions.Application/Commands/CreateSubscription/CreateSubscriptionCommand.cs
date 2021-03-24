using MediatR;

namespace Subscriptions.Application.Commands.CreateSubscription
{
    public class CreateSubscriptionCommand : IRequest<CreateSubscriptionCommandResponse>
    {
        public string OfferId { get; set; }
        public string SubscriberId { get; set; }
    }
}