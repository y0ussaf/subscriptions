using MediatR;

namespace Subscriptions.Application.Commands.RegisterBackendApp
{
    public class RegisterBackendAppCommand : IRequest<RegisterBackendAppResponse>
    {
        public string Name { get; set; }
    }
}