using MediatR;
using Product.Application.Events;
using Product.Application.Exceptions;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public record UpdateProductStatusRequest(int Id, ProductStatus ProductStatus) : IRequest<int>
    {
        public class UpdateProductStatusRequestHandler : IRequestHandler<UpdateProductStatusRequest, int>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public UpdateProductStatusRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<int> Handle(UpdateProductStatusRequest request, CancellationToken cancellationToken)
            {
                var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

                _ = product ?? throw new NotFoundException($"Product {request.Id} Not Found.");

                var updatedProduct = product.ChangeProductStatus(request.ProductStatus);

                // Add Domain Events to be raised after the commit
                product.DomainEvents.Add(new ProductStatusUpdatedDomainEvent(product));

                await _repository.UpdateAsync(updatedProduct, cancellationToken);

                return request.Id;
            }
        }

    }
}
