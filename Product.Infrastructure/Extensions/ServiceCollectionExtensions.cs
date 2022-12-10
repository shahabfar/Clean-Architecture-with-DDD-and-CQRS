using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Interfaces;
using Product.Infrastructure.Events;
using Product.Infrastructure.Mapping;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            MapsterSettings.Configure();
            return services
            .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")))
            .AddRepositories()
            //.AddScoped<ICurrentUser, CurrentUser>()
            .AddTransient<IEventPublisher, EventPublisher>();
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(ApplicationDbRepository<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ApplicationDbRepository<>));
            return services;
        }
    }
}
