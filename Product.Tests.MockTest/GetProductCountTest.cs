using Moq;
using Product.Application.Features.Products;
using Product.Application.Interfaces;
using Product.Domain.Enums;
using static Product.Application.Features.Products.GetProductGroupCountRequest;

namespace Product.Tests.MockTest
{
    public class GetProductCountTest
    {
        private Mock<IRepository<Domain.Entities.Product>>? _mockRepo;
        private GetProductGroupCountRequestHandler? _handler;

        [Fact]
        public async Task GetProductCountByStatus_WhenCountIsCorrect()
        {
            // Arrange
            var productStatus = ProductStatus.Damaged;
            _mockRepo = MockProductRepository.GetProductRepository(productStatus);
            _handler = new GetProductGroupCountRequestHandler(_mockRepo.Object);

            // Act
            var result = await _handler.Handle(new GetProductGroupCountRequest(productStatus), CancellationToken.None);
            var products = await _mockRepo.Object.ListAsync(default);

            //Assert
            Assert.IsType<int>(result);
            Assert.Equal(result, products.Count(p => p.ProductStatus == productStatus));
        }
    }
}
