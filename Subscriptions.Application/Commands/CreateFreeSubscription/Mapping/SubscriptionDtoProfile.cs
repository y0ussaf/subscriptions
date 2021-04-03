using AutoMapper;
using Subscriptions.Domain.Entities;

namespace Subscriptions.Application.Commands.CreateFreeSubscription.Mapping
{
    public class SubscriptionDtoProfile : Profile
    {
        public SubscriptionDtoProfile()
        {
            CreateMap<Subscription, SubscriptionDto>();
        }
    }
}