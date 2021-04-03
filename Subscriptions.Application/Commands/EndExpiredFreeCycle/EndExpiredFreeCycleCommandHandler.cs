using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Common;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.EndExpiredFreeCycle
{
    public class EndExpiredFreeCycleCommandHandler : IRequestHandler<EndExpiredFreeCycleCommand,EndExpiredFreeCycleCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public EndExpiredFreeCycleCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<EndExpiredFreeCycleCommandResponse> Handle(EndExpiredFreeCycleCommand request, CancellationToken cancellationToken)
        {
            // app owner 
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var cycle = await _subscriptionsRepository.GetCycle(request.CycleId);
                    if (cycle is null)
                    {
                        throw new NotFoundException("");
                    }

                    if (cycle.DateTimeRange.End is not null)
                    {
                        throw new BadRequestException("");
                    }
                    var paidOffer = cycle.Subscription.PaidOffer;
                    DateTime cycleEnd;
                    var now = DateTime.Now;
                    if (paidOffer is PaidOfferWithSpecificDayOnWitchCyclesStart)
                    {
                        cycleEnd = (paidOffer as PaidOfferWithSpecificDayOnWitchCyclesStart).GetCurrentCycleStartingDateTime(now);
                    }
                    else
                    {
                        cycleEnd = now;
                    }
                   
           
                    cycle.DateTimeRange = new DateTimeRange(cycle.DateTimeRange.Start, cycleEnd);
                    await _subscriptionsRepository.SetCycleDateTimeRange(cycle);
                    await unitOfWork.CommitWork();
                    return new EndExpiredFreeCycleCommandResponse();
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