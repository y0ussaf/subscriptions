using FluentValidation;

namespace Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite
{
    public class TransformInfiniteIntervalIntroFiniteCommandValidator : AbstractValidator<TransformInfiniteIntervalIntoFiniteCommand>
    {
        public TransformInfiniteIntervalIntroFiniteCommandValidator()
        {
            RuleFor(x => x.TimelineId)
                .NotNull();
        }
    }
}