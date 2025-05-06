using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using UniqueShit.Application.Core.Behaviors;

namespace UniqueShit.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationAssemblyReference).Assembly;

            services.AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssembly(applicationAssembly, includeInternalTypes: true);

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(applicationAssembly);

                configuration.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                configuration.AddOpenBehavior(typeof(TransactionBehaviour<,>));
            });

            return services;
        }
    }
}
