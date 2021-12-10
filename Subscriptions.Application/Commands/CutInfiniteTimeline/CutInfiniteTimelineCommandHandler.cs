using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Commands.EndExpiredFreeCycle;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;

namespace Subscriptions.Application.Commands.CutInfiniteTimeline
{
    public class CutInfiniteTimelineCommandHandler : IRequestHandler<CutInfiniteTimelineCommand,CutInfiniteTimelineCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public CutInfiniteTimelineCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<CutInfiniteTimelineCommandResponse> Handle(CutInfiniteTimelineCommand request, CancellationToken cancellationToken)
        {
            // app owner 
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var timeLine = await _subscriptionsRepository.GetTimeline(request.TimelineId);
                    if (timeLine is null)
                    {
                        throw new NotFoundException("");
                    }

                    if (timeLine.DateTimeRange.End is not null)
                    {
                        throw new BadRequestException("");
                    }
                    var now = DateTime.Now;
                    timeLine.DateTimeRange = new DateTimeRange(timeLine.DateTimeRange.Start, now);
                    await _subscriptionsRepository.SetCycleDateTimeRange(timeLine);
                    await unitOfWork.CommitWork();
                    return new CutInfiniteTimelineCommandResponse();
                }
                catch (Exception)
                {
                    await unitOfWork.RollBack();
                    throw;
                }
            } 
        }
    }
}