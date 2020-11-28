using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.CachingProvider
{
    public static class CachingProviderConfiguration
    {
        public static void CachingConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddTransient<ICachingProviderService, MemoryCacheProvider>();
        }
    }
}