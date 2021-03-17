using AutoMapper;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.RegisterBackendApp.Mapping
{
    public class RegisterBackendAppCommandProfile : Profile
    {
        public RegisterBackendAppCommandProfile()
        {
            CreateMap<RegisterBackendAppCommand, App>();
        }
    }
}