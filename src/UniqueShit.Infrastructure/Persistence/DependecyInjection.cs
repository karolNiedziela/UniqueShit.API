using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniqueShit.Application.Core.Persistence;
using UniqueShit.Domain.Repositories;
using UniqueShit.Infrastructure.Persistence.Options;
using UniqueShit.Infrastructure.Persistence.Repositories;

namespace UniqueShit.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ConnectionStringOptions.SectionName);

            services.AddSingleton(new ConnectionStringOptions(connectionString!));

            services.AddDbContext<UniqueShitDbContext>(options => 
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(UniqueShitDbContext).Assembly.FullName))
            );

            services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetRequiredService<UniqueShitDbContext>());

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<UniqueShitDbContext>());

            services.AddScoped<IOfferRepository, OfferRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IBrandRepository, ManufacturerRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IFavouriteOfferRepository, FavouriteOfferRepository>();


            services.AddHostedService<MigratorHostedService>();

            return services;
        }
    }
}
