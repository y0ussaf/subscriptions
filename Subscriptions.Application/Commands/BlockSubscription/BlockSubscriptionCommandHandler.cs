using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.BlockSubscription
{
    public class BlockSubscriptionCommandHandler : IRequestHandler<BlockSubscriptionCommand,BlockSubscriptionCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ISubscriptionsRepository _subscriptionsRepository;

        public BlockSubscriptionCommandHandler(IUnitOfWorkContext unitOfWorkContext,ISubscriptionsRepository subscriptionsRepository)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _subscriptionsRepository = subscriptionsRepository;
        }
        public async Task<BlockSubscriptionCommandResponse> Handle(BlockSubscriptionCommand request, CancellationToken cancellationToken)
        {
            // only app owner 

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
                    subscription.Blocked = true;
                    await _subscriptionsRepository.SetBlockingStatus(subscription);
                    await unitOfWork.CommitWork();
                    return new BlockSubscriptionCommandResponse();
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