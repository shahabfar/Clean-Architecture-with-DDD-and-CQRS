using Ardalis.Specification;
using MediatR;
using Product.Application.DTOs;
using Product.Application.Exceptions;
using Product.Application.Interfaces;

namespace Product.Application.Features.Products
{
    public record GetProductRequest(int Id) : IRequest<ProductDto>
    {
        public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductDto>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public GetProductRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<ProductDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
            {
                return await _repository.FirstOrDefaultAsync((ISpecification<Domain.Entities.Product, ProductDto>)new ProductByIdWithCategorySpec(request.Id), cancellationToken)
                ?? throw new NotFoundException($"Product {request.Id} Not Found.");
            }
        }
    }
}
