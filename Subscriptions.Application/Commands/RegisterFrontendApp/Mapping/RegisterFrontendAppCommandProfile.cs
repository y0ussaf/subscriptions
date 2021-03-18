using AutoMapper;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.RegisterFrontendApp.Mapping
{
    public class RegisterFrontendAppCommandProfile : Profile
    {
        public RegisterFrontendAppCommandProfile()
        {
            CreateMap<RegisterFrontendAppCommand, FrontendApp>();
        }
    }
}