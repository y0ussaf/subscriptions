using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateFeature
{
    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand,CreateFeatureCommandResponse>
    {
        private readonly IUnitOfWorkContext _unitOfWorkContext;
        private readonly ICreateFeaturePersistence _createFeaturePersistence;

        public CreateFeatureCommandHandler(IUnitOfWorkContext unitOfWorkContext,ICreateFeaturePersistence createFeaturePersistence)
        {
            _unitOfWorkContext = unitOfWorkContext;
            _createFeaturePersistence = createFeaturePersistence;
        }

        public async Task<CreateFeatureCommandResponse> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {
            
           var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
           await unitOfWork.BeginWork();
           try
           {
               var feature = new Feature()
               {
                   Name = request.Name
               };
               var id = await _createFeaturePersistence.CreateFeature(feature);
               await unitOfWork.CommitWork();
               return new CreateFeatureCommandResponse()
               {
                   Id = id
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