using Moq;
using Product.Application.Features.Products;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Tests.MockTest
{
    public static class MockProductRepository
    {
        public static Mock<IRepository<Domain.Entities.Product>> GetProductRepository(ProductStatus status = ProductStatus.InStock)
        {
            var products = new List<Domain.Entities.Product>
            {
                    new Domain.Entities.Product(1, "Product1", "ABC123456", "description of product1", true, ProductStatus.InStock, 1),
                    new Domain.Entities.Product(2, "Product2", "DEF789101", "description of product2", true, ProductStatus.Sold, 1),
                    new Domain.Entities.Product(3, "Product3", "GHI121314", "description of product3", true, ProductStatus.Damaged, 1),
                    new Domain.Entities.Product(4, "Product4", "JKL151617", "description of product4", true, ProductStatus.InStock, 1),
                    new Domain.Entities.Product(5, "Product5", "MNO181920", "description of product5", true, ProductStatus.InStock, 1)
            };

            var foundProduct = products.FirstOrDefault(p => p.Id == 1);
            var count = products.Count(p => p.ProductStatus == status);
            var mockRepo = new Mock<IRepository<Domain.Entities.Product>>();

            //mockRepo.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.Product>(), default)).ReturnsAsync((Domain.Entities.Product product) =>
            //{
            //    products.Add(product);
            //    return product;
            //});

            mockRepo.Setup(r => r.ListAsync(default)).ReturnsAsync(products);
            mockRepo.Setup(r => r.CountAsync(It.IsAny<ProductGroupSpec>(), default)).ReturnsAsync(count);
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>(), default)).ReturnsAsync(foundProduct);

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Domain.Entities.Product>(), default)).Callback(() =>
            {
                foundProduct?.ChangeProductStatus(ProductStatus.Sold);
            });

            return mockRepo;
        }
    }
}
