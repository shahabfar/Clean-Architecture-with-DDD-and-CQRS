using Moq;
using Product.Application.Features.Products;
using Product.Application.Interfaces;
using Product.Domain.Enums;
using static Product.Application.Features.Products.UpdateProductStatusRequest;

namespace Product.Tests.MockTest
{
    public class SellProductTest
    {
        private Mock<IRepository<Domain.Entities.Product>>? _mockRepo;
        private UpdateProductStatusRequestHandler? _handler;

        [Fact]
        public async Task SellProduct_ProductStatusShouldBeSold()
        {
            // Arrange
            var productStatus = ProductStatus.Sold;
            _mockRepo = MockProductRepository.GetProductRepository();
            _handler = new UpdateProductStatusRequestHandler(_mockRepo.Object);

            // Act
            var result = await _handler.Handle(new UpdateProductStatusRequest(1, productStatus), CancellationToken.None);
            var product = await _mockRepo.Object.GetByIdAsync(1, default);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(productStatus, product?.ProductStatus);
        }
    }
}
