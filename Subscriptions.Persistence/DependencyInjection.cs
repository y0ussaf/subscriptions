using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Subscriptions.Application.Common.Interfaces;

namespace Subscriptions.Persistence
{
    public static class DependencyInjection
    {
        public  static IServiceCollection AddPersistence(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped(_ =>
            {
                var conStr = configuration.GetSection("")[""];
                return new SqlConnection(conStr);
            });
            serviceCollection.AddScoped<IUnitOfWorkContext,UnitOfWorkContext>();
 
            return serviceCollection;
        }
    }
}