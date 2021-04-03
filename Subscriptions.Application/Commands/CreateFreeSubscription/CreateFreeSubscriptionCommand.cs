using MediatR;

namespace Subscriptions.Application.Commands.CreateFreeSubscription
{
    public class CreateFreeSubscriptionCommand : IRequest<CreateFreeSubscriptionCommandResponse>
    {
        public string OfferId { get; set; }
        public string SubscriberId { get; set; }
    }
}