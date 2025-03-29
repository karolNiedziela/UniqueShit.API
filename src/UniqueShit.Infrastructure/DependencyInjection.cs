using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Infrastructure.Authentication;
using UniqueShit.Infrastructure.Persistence;

namespace UniqueShit.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration);

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            return services;
        }
    }
}
