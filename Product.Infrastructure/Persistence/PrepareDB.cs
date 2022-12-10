using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Enums;
using Product.Infrastructure.Persistence;

namespace Product.infrastructure.Persistence
{
    public static class PrepareDB
    {
        public static void CreateAndSeedDB(IApplicationBuilder app)
        {
            Console.WriteLine("Check for database existence...");
            using var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context == null)
                throw new NullReferenceException("ApplicationDbContext service cannot be retrieved");

            SeedData(context);
        }

        private static void SeedData(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            Console.WriteLine("Adding initial data...");

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Domain.Entities.Category(1, "category1", "description of category1"));

                context.SaveChanges();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Domain.Entities.Product(1, "Product1", "ABC123456", "description of product1", true, ProductStatus.InStock, 1),
                    new Domain.Entities.Product(2, "Product2", "DEF789101", "description of product2", true, ProductStatus.Sold, 1),
                    new Domain.Entities.Product(3, "Product3", "GHI121314", "description of product3", true, ProductStatus.Damaged, 1),
                    new Domain.Entities.Product(4, "Product4", "JKL151617", "description of product4", true, ProductStatus.InStock, 1),
                    new Domain.Entities.Product(5, "Product5", "MNO181920", "description of product5", true, ProductStatus.InStock, 1));

                context.SaveChanges();
            }
        }
    }
}
