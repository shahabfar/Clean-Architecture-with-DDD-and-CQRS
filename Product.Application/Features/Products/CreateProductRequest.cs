using MediatR;
using Product.Application.Events;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public record CreateProductRequest(string Name, string Barcode, string? Description, bool IsWeighted, ProductStatus ProductStatus, int CategoryId) : IRequest<int>
    {
        public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, int>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public CreateProductRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<int> Handle(CreateProductRequest request, CancellationToken cancellationToken)
            {
                var product = new Domain.Entities.Product(request.Name, request.Barcode, request.Description, request.IsWeighted, request.ProductStatus, request.CategoryId);

                // Add Domain Events to be raised after the commit
                product.DomainEvents.Add(new ProductCreatedDomainEvent(product));

                var addedProduct = await _repository.AddAsync(product, cancellationToken);
                return addedProduct.Id;
            }
        }
    }
}
