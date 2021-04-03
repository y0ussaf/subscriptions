using MediatR;

namespace Subscriptions.Application.Commands.EndExpiredFreeCycle
{
    public class EndExpiredFreeCycleCommand : IRequest<EndExpiredFreeCycleCommandResponse>
    {
        public string CycleId { get; set; }
    }
}