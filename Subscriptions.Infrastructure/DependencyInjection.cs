using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscriptions.Application.Common.Interfaces;
using Subscriptions.Application.Common.Services.Stripe.Interfaces;
using Subscriptions.Infrastructure.Stripe;

namespace Subscriptions.Infrastructure
{
    public static class DependencyInjection
    {
        public  static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,IConfiguration configuration)
        {

            serviceCollection.AddScoped<IPaymentIntentService, PaymentIntentService>();
            serviceCollection.AddScoped<IPaymentMethodsService, PaymentMethodsService>();
            return serviceCollection;
        }
    }
}