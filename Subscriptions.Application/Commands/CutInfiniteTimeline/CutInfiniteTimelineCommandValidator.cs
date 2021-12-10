using FluentValidation;
using Subscriptions.Application.Commands.EndExpiredFreeCycle;

namespace Subscriptions.Application.Commands.CutInfiniteTimeline
{
    public class CutInfiniteTimelineCommandValidator : AbstractValidator<CutInfiniteTimelineCommand>
    {
        public CutInfiniteTimelineCommandValidator()
        {
            RuleFor(x => x.TimelineId)
                .NotNull();
        }
    }
}