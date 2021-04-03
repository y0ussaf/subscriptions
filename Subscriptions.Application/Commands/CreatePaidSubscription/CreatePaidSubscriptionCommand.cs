using MediatR;

namespace Subscriptions.Application.Commands.CreatePaidSubscription
{
    public class CreatePaidSubscriptionCommand : IRequest<CreatePaidSubscriptionCommandResponse>
    {
        public string OfferId { get; set; }
        public string SubscriberId { get; set; }
    }
}