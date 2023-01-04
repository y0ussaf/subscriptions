using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Exceptions;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Queries.Plans.GetPlan
{ 
    public class GetPlanQueryHandler : IRequestHandler<GetPlanQuery,GetPlanQueryResponse>
    {
        private readonly IGetPlanPersistence _getPlanPersistence;
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetPlanQueryHandler(IGetPlanPersistence getPlanPersistence,IUnitOfWorkContext unitOfWorkContext)
        {
            _getPlanPersistence = getPlanPersistence;
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<GetPlanQueryResponse> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                var plan = await _getPlanPersistence.GetPlan(request.PlanId);
                if (plan is null)
                {
                    throw new NotFoundException("plan not found");
                }
                return new GetPlanQueryResponse()
                {
                    Plan = plan
                };
            }
            catch (Exception)
            {
                await unitOfWork.RollBack();
                throw;
            }
        }
    }
}