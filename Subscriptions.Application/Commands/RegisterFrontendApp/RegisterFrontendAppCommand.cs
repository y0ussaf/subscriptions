using MediatR;

namespace Subscriptions.Application.Commands.RegisterFrontendApp
{
    public class RegisterFrontendAppCommand : IRequest<RegisterFrontendAppResponse>
    {
        public string Name { get; set; }

    }
}