using MediatR;
using Subscriptions.Application.Commands.EndExpiredFreeCycle;

namespace Subscriptions.Application.Commands.CutInfiniteTimeline
{
    public class CutInfiniteTimelineCommand : IRequest<CutInfiniteTimelineCommandResponse>
    {
        public string TimelineId { get; set; }
    }
}