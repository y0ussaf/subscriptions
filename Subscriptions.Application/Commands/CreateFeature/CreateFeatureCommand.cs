using MediatR;

namespace Subscriptions.Application.Commands.CreateFeature
{
    public class CreateFeatureCommand : IRequest<CreateFeatureCommandResponse>
    {
        public string Name { get; set; }
    }
}