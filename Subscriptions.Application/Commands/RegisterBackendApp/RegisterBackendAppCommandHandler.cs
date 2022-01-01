using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.RegisterBackendApp
{
    public class RegisterBackendAppCommandHandler : IRequestHandler<RegisterBackendAppCommand,RegisterBackendAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppsRepository _appsRepository;
        private readonly IAppSecretGenerator _appSecretGenerator;
        private readonly IUnitOfWorkContext _unitOfWorkContext;

        public RegisterBackendAppCommandHandler(IMapper mapper,IAppsRepository appsRepository,IAppSecretGenerator appSecretGenerator
        ,IUnitOfWorkContext unitOfWorkContext)
        {
            _mapper = mapper;
            _appsRepository = appsRepository;
            _appSecretGenerator = appSecretGenerator;
            _unitOfWorkContext = unitOfWorkContext;
        }

        public async Task<RegisterBackendAppResponse> Handle(RegisterBackendAppCommand request, CancellationToken cancellationToken)
        {
            var unitOfWork = await _unitOfWorkContext.CreateUnitOfWork();
            var app = new BackendApp();
            _mapper.Map(request,app);
            app.Secret = _appSecretGenerator.GenerateKey();
            try
            {
                await unitOfWork.BeginWork();
                var id = await _appsRepository.RegisterBackendApp(app);
                return new RegisterBackendAppResponse
                {
                    Secret = app.Secret,
                    Id = id
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}