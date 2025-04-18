using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniqueShit.Application.Core.Authentication;
using UniqueShit.Infrastructure.Authentication.Options;

namespace UniqueShit.Infrastructure.Authentication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CreateAppUserEndpointFilterOptions>(configuration.GetSection(CreateAppUserEndpointFilterOptions.SectionName));

            services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

            return services;
        }
    }
}
