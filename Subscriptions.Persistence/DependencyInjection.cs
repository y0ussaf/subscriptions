using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Persistence.Repositories;

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

            serviceCollection.AddScoped<IAppsRepository, AppsRepository>();
            serviceCollection.AddScoped<IFeaturesRepository, FeaturesRepository>();
            serviceCollection.AddScoped<IInvoicesRepository, InvoicesRepository>();
            serviceCollection.AddScoped<IPaymentsRepository, PaymentsRepository>();
            serviceCollection.AddScoped<IPaymentMethodsRepository, PaymentMethodsRepository>();
            serviceCollection.AddScoped<IPlansRepository, PlansRepository>();
            serviceCollection.AddScoped<ISubscribersRepository, SubscribersRepository>();
            serviceCollection.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
            serviceCollection.AddScoped<IOffersRepository, OffersRepository>();
            return serviceCollection;
        }
    }
}