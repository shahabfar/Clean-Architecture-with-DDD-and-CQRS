using Microsoft.AspNetCore.Builder;
using Product.infrastructure.Persistence;

namespace Product.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
        {
            PrepareDB.CreateAndSeedDB(builder);
            return builder;
        }
    }
}
