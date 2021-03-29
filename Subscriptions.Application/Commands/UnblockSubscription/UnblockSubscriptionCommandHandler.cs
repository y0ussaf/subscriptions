using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Commands.UnblockSubscription
{
    public class UnblockSubscriptionCommandHandler : IRequestHandler<UnblockSubscriptionCommand,UnblockSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public UnblockSubscriptionCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<UnblockSubscriptionCommandResponse> Handle(UnblockSubscriptionCommand request, CancellationToken cancellationToken)
        {
            // only app owner 
            await using (var unitOfWork = await  _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var subscription =  await _subscriptionsRepository.GetSubscription(request.SubscriptionId);
                    if (subscription is null)
                    {
                        throw new NotFoundException("");
                    }
                    subscription.Blocked = false;
                    await _subscriptionsRepository.SetBlockingStatus(subscription);
                    return new UnblockSubscriptionCommandResponse();
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