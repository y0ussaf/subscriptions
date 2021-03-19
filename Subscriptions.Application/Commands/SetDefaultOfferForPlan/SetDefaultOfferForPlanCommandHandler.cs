using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Commands.SetDefaultOfferForPlan
{
    public class SetDefaultOfferForPlanCommandHandler : IRequestHandler<SetDefaultOfferForPlanCommand>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly IPlansRepository _plansRepository;
        private readonly IOffersRepository _offersRepository;

        public SetDefaultOfferForPlanCommandHandler(IUnitOfWorkContext unitOfWorkContext,
            IPlansRepository plansRepository,
            IOffersRepository offersRepository
            )
        {
            _unitOfWorkContext = unitOfWorkContext;
            _plansRepository = plansRepository;
            _offersRepository = offersRepository;
        }

        public async Task<Unit> Handle(SetDefaultOfferForPlanCommand request, CancellationToken cancellationToken)
        {
            await using (var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork())
            {
                await unitOfWork.BeginWork();
                try
                {
                    var plan = await _plansRepository.GetPlan(request.PlanId);
                    if (plan is null)
                    {
                        throw new NotFoundException("");
                    }
                    var offer = await _offersRepository.GetOffer(request.OfferId);
                    await _offersRepository.SetDefaultOfferForPlan(plan.Id, offer.Id);
                    await unitOfWork.CommitWork();
                    return Unit.Value;
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