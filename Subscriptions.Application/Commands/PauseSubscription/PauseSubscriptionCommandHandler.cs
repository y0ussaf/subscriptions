using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.PauseSubscription
{
    public class PauseSubscriptionCommandHandler : IRequestHandler<PauseSubscriptionCommand,PauseSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public PauseSubscriptionCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<PauseSubscriptionCommandResponse> Handle(PauseSubscriptionCommand request, CancellationToken cancellationToken)
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

                    if (!subscription.IsActive())
                    {
                        throw new BadRequestException("you can pause only active subscription");
                    }

                    subscription.Status = SubscriptionStatus.Paused;
                    await _subscriptionsRepository.SetStatus(subscription);
                    await unitOfWork.CommitWork();
                    return new PauseSubscriptionCommandResponse();
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