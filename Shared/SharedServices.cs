using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.CachingProvider;
using Shared.DbInit;
using Shared.DbInit.Repository;
//using Shared.Notification;

namespace Shared
{
    public static class SharedServices
    {
        public static void AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.DatabaseInitialization(configuration);
            services.CachingConfiguration(configuration);
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}