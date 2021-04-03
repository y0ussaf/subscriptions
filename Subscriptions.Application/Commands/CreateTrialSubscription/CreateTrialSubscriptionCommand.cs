using MediatR;

namespace Subscriptions.Application.Commands.CreateTrialSubscription
{
    public class CreateTrialSubscriptionCommand : IRequest<CreateTrialSubscriptionCommandResponse>
    {
        public string OfferId { get; set; }
        public string SubscriberId { get; set; }
    }
}