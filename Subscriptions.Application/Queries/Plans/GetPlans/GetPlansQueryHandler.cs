using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Application.Queries.Plans.GetPlans
{ 
    public class GetPlansQueryHandler : IRequestHandler<GetPlanQuery,GetPlansQueryResponse>
    {
        private readonly IGetPlanPersistence _getPlanPersistence;
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public GetPlansQueryHandler(IGetPlanPersistence getPlanPersistence,IUnitOfWorkContext unitOfWorkContext)
        {
            _getPlanPersistence = getPlanPersistence;
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<GetPlansQueryResponse> Handle(GetPlanQuery request, CancellationToken cancellationToken)
        {
            var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            await unitOfWork.BeginWork();
            try
            {
                var plan = await _getPlanPersistence.GetPlan(request.PlanId);
                return new GetPlansQueryResponse()
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