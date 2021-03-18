using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.RegisterFrontendApp
{
    public class RegisterFrontendAppCommandHandler : IRequestHandler<RegisterFrontendAppCommand,RegisterFrontendAppResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAppsRepository _appsRepository;

        public RegisterFrontendAppCommandHandler(IMapper mapper,IAppsRepository appsRepository)
        {
            _mapper = mapper;
            _appsRepository = appsRepository;
        }

        public async Task<RegisterFrontendAppResponse> Handle(RegisterFrontendAppCommand request, CancellationToken cancellationToken)
        {
            var frontendApp = new FrontendApp();
            _mapper.Map(request, frontendApp);
            frontendApp.Id = Guid.NewGuid().ToString();
            await _appsRepository.RegisterFrontendApp(frontendApp);
            return new RegisterFrontendAppResponse()
            {
                Id = frontendApp.Id
            };
        }
    }
}