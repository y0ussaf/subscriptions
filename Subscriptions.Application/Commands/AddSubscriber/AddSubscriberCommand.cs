using MediatR;

namespace Subscriptions.Application.Commands.AddSubscriber
{
    public class AddSubscriberCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}