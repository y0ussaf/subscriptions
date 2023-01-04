using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite.Persistence;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite
{
    public class TransformInfiniteIntervalIntoFiniteCommandHandler : IRequestHandler<TransformInfiniteIntervalIntoFiniteCommand,TransformInfiniteIntervalIntoFiniteResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ITransformInfiniteTimelineIntoFinitePersistence _persistence;

        public TransformInfiniteIntervalIntoFiniteCommandHandler(IUnitOfWorkContext unitOfWorkContext,ITransformInfiniteTimelineIntoFinitePersistence persistence)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _persistence = persistence;
        }

        public async Task<TransformInfiniteIntervalIntoFiniteResponse> Handle(TransformInfiniteIntervalIntoFiniteCommand request, CancellationToken cancellationToken)
        {
            // app owner 
            await using var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                var timeLine = await _persistence.GetInterval(request.TimelineId);
                if (timeLine is null)
                {
                    throw new NotFoundException("");
                }
                    
                if (timeLine.DateTimeRange.End is not null)
                {
                    throw new BadRequestException("");
                }
                var now = DateTime.Now;
                await _persistence.SetIntervalEnd(request.TimelineId, now);
                await unitOfWork.CommitWork();
                return new TransformInfiniteIntervalIntoFiniteResponse();
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}