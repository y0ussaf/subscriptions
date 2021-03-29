using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Commands.PauseSubscription;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.ResumeSubscription
{
    public class ResumeSubscriptionCommandHandler : IRequestHandler<ResumeSubscriptionCommand,ResumeSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public ResumeSubscriptionCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<ResumeSubscriptionCommandResponse> Handle(ResumeSubscriptionCommand request, CancellationToken cancellationToken)
        {
            //subscription owner or admin are allowed
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var subscription = await _subscriptionsRepository.GetSubscription(request.SubscriptionId);
                    if (subscription is null)
                    {
                        throw new NotFoundException("");
                    }

                    if ( subscription.Status != SubscriptionStatus.Paused)
                    {
                        throw new BadRequestException("you can resume only paused subscription");
                    }

                    subscription.Status = SubscriptionStatus.Active;
                    await _subscriptionsRepository.SetStatus(subscription);
                    await unitOfWork.CommitWork();
                    return new ResumeSubscriptionCommandResponse();
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