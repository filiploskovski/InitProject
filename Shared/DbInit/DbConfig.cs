using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.DbInit
{
    public static class DbConfig
    {
        public static void DatabaseInitialization(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration["DatabaseConnectionStrings:DatabaseProvider"];
            var databaseString = configuration["DatabaseConnectionStrings:ConnectionString"];

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(databaseString)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            });
        }
    }
}