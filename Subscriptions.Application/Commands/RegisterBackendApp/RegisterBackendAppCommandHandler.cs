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

        public RegisterBackendAppCommandHandler(IMapper mapper,IAppsRepository appsRepository,IAppSecretGenerator appSecretGenerator)
        {
            _mapper = mapper;
            _appsRepository = appsRepository;
            _appSecretGenerator = appSecretGenerator;
        }

        public async Task<RegisterBackendAppResponse> Handle(RegisterBackendAppCommand request, CancellationToken cancellationToken)
        {
            var app = new BackendApp();
            _mapper.Map(request,app);
            var secret = _appSecretGenerator.GenerateKey();
            app.Secret = Encoding.UTF8.GetString(MD5.HashData(Encoding.UTF8.GetBytes(secret)));
            var id = await _appsRepository.RegisterBackendApp(app);
            return new RegisterBackendAppResponse
            {
                Secret = secret,
                Id = id
            };
        }
    }
}