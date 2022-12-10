using MediatR;
using Product.Application.Exceptions;
using Product.Application.Interfaces;

namespace Product.Application.Features.Products
{
    public record DeleteProductRequest(int Id) : IRequest<int>
    {
        public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, int>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public DeleteProductRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<int> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
            {
                var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
                _ = product ?? throw new NotFoundException($"Product {request.Id} Not Found.");

                // delete domain event
                //product.DomainEvents.Add(new ProductDeletedDomainEvent(product));

                await _repository.DeleteAsync(product, cancellationToken);
                return request.Id;
            }
        }
    }
}
