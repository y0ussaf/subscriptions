using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Subscriptions.Application.Commands.AddFeatureToPlan.Persistence;
using Subscriptions.Application.Commands.AddOfferToPlan.Persistence;
using Subscriptions.Application.Commands.AddPlan.Persistence;
using Subscriptions.Application.Commands.CreateSubscription.Persistence;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Persistence.Commands.AddFeatureToPlan;
using Subscriptions.Persistence.Commands.AddOffer;
using Subscriptions.Persistence.Commands.AddPlan;
using Subscriptions.Persistence.Commands.CreateSubscription;

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
            serviceCollection.AddScoped<IUnitOfWorkContext,UnitOfWorkContext>();

            serviceCollection.AddScoped<IAddOfferToPlanCommandPersistence, AddOfferPersistence>();
            serviceCollection.AddScoped<ICreateSubscriptionCommandPersistence, CreateSubscriptionPersistence>();
            serviceCollection.AddScoped<IAddPlanCommandPersistence, AddPlanPersistence>();
            serviceCollection.AddScoped<IAddFeatureToPlanCommandPersistence,AddFeatureToPlanPersistence>();
            return serviceCollection;
        }
    }
}