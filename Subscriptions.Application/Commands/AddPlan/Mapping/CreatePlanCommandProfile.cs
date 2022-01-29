using AutoMapper;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.AddPlan.Mapping
{
    public class CreatePlanCommandProfile : Profile
    {
        public CreatePlanCommandProfile()
        {
            CreateMap<CreatePlanCommand, Plan>();
        }
    }
}