using AutoMapper;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateTrialSubscription.Mapping
{
    public class SubscriptionDtoProfile : Profile
    {
        public SubscriptionDtoProfile()
        {
            CreateMap<Subscription, CreateFreeSubscription.SubscriptionDto>();
        }
    }
}