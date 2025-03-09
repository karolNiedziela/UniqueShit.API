using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniqueShit.Infrastructure.Persistence;

namespace UniqueShit.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            return services;
        }
    }
}
