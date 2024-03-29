﻿using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Subscriptions.Application.Commands.AddFeatureToPlan.Persistence;
using Subscriptions.Application.Commands.AddOfferToPlan.Persistence;
using Subscriptions.Application.Commands.AddSubscriber.Persistence;
using Subscriptions.Application.Commands.CreateFeature;
using Subscriptions.Application.Commands.CreatePlan.Persistence;
using Subscriptions.Application.Commands.CreateSubscription.Persistence;
using Subscriptions.Application.Commands.PauseSubscription.Persistence;
using Subscriptions.Application.Commands.SetDefaultPlan.Persistence;
using Subscriptions.Application.Commands.TransformInfiniteIntervalIntroFinite.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Persistence;
using Subscriptions.Application.Queries.Offers.GetIntervalsDefinitions.Persistence;
using Subscriptions.Application.Queries.Offers.GetOffer.Persistence;
using Subscriptions.Application.Queries.Offers.GetOffers.Persistence;
using Subscriptions.Application.Queries.Plans.GetPlan;
using Subscriptions.Application.Queries.Plans.GetPlans.Persistence;
using Subscriptions.Persistence.Commands.AddFeatureToPlan;
using Subscriptions.Persistence.Commands.AddOffer;
using Subscriptions.Persistence.Commands.AddPlan;
using Subscriptions.Persistence.Commands.AddSubscriber;
using Subscriptions.Persistence.Commands.CreateFeature;
using Subscriptions.Persistence.Commands.CreateSubscription;
using Subscriptions.Persistence.Commands.PauseSubscription;
using Subscriptions.Persistence.Commands.SetDefaultPlan;
using Subscriptions.Persistence.Commands.TransformInfiniteTimelineIntoFinite;
using Subscriptions.Persistence.Common;
using Subscriptions.Persistence.Queries.Offers.GetOffer;
using Subscriptions.Persistence.Queries.Offers.GetOffers;
using Subscriptions.Persistence.Queries.Offers.GetTimelinesDefinitions;
using Subscriptions.Persistence.Queries.Plans.GetPlan;
using Subscriptions.Persistence.Queries.Plans.GetPlans;

namespace Subscriptions.Persistence
{
    public static class DependencyInjection
    {
        public  static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped(_ =>
            {
                var conStr = configuration.GetConnectionString("Subscriptions_db");
                return new NpgsqlConnection(conStr);
            });
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

            serviceCollection.AddScoped<IUnitOfWorkContext,UnitOfWorkContext>();

            serviceCollection.AddScoped<IAddOfferToPlanCommandPersistence, AddOfferPersistence>();
            serviceCollection.AddScoped<ICreateSubscriptionCommandPersistence, CreateSubscriptionPersistence>();
            serviceCollection.AddScoped<IAddPlanCommandPersistence, AddPlanPersistence>();
            serviceCollection.AddScoped<IAddFeatureToPlanCommandPersistence,AddFeatureToPlanPersistence>();
            serviceCollection.AddScoped<IAddSubscriberPersistence, AddSubscriberPersistence>();
            serviceCollection.AddScoped<IGetOfferQueryPersistence, GetOfferQueryPersistence>();
            serviceCollection.AddScoped<IGetTimelinesDefinitionsPersistence, GetIntervalsDefinitionsPersistence>();
            serviceCollection.AddScoped<ISubscribersPersistence, SubscribersPersistence>();
            serviceCollection.AddScoped<IPlansPersistence, PlansPersistence>();
            serviceCollection.AddScoped<ITransformInfiniteTimelineIntoFinitePersistence, TransformInfiniteIntervalIntoFinitePersistence>();
            serviceCollection.AddScoped<ISetDefaultPlanCommandPersistence, SetDefaultPlanCommandPersistence>();
            serviceCollection.AddScoped<IPauseSubscriptionCommandPersistence, PauseSubscriptionCommandPersistence>();
            serviceCollection.AddScoped<IGetPlanPersistence, GetPlanQueryPersistence>();
            serviceCollection.AddScoped<ICreateFeaturePersistence, CreateFeaturePersistence>();
            serviceCollection.AddScoped<IGetPlansQueryPersistence, GetPlansQueryPersistence>();
            serviceCollection.AddScoped<IGetOffersQueryPersistence, GetOffersQueryPersistence>();
            
            return serviceCollection;
        }
    }
}