using MediatR;
using Product.Application.DomainEvents.Events;
using Product.Application.Exceptions;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public record UpdateProductRequest(int Id, string Name, string Barcode, string? Description, bool IsWeighted, ProductStatus ProductStatus, int CategoryId) : IRequest<int>
    {
        public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, int>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public UpdateProductRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<int> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
            {
                var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

                _ = product ?? throw new NotFoundException($"Product {request.Id} Not Found.");

                var updatedProduct = product.Update(request.Name, request.Barcode, request.Description, request.IsWeighted, request.ProductStatus, request.CategoryId);

                // Add Domain Events to be raised after the commit
                product.DomainEvents.Add(new ProductUpdatedDomainEvent(product));

                await _repository.UpdateAsync(updatedProduct, cancellationToken);

                return request.Id;
            }
        }
    }
}
