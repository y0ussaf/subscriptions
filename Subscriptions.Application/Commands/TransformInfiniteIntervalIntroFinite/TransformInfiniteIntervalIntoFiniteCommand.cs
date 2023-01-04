using MediatR;

namespace Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite
{
    public class TransformInfiniteIntervalIntoFiniteCommand : IRequest<TransformInfiniteIntervalIntoFiniteResponse>
    {
        public string TimelineId { get; set; }
    }
}