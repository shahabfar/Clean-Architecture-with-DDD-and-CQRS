using MediatR;
using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public class GetProductsCountRequest : IRequest<ProductsCountDto>
    {
        public class GetProductsCountRequestHandler : IRequestHandler<GetProductsCountRequest, ProductsCountDto>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public GetProductsCountRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<ProductsCountDto> Handle(GetProductsCountRequest request, CancellationToken cancellationToken)
            {
                var products = await _repository.ListAsync(cancellationToken);
                var counts = products.GroupBy(g => 1).Select(p => new ProductsCountDto
                {
                    Sold = p.Where(c => c.ProductStatus == ProductStatus.Sold).Count(),
                    InStock = p.Where(c => c.ProductStatus == ProductStatus.InStock).Count(),
                    Damaged = p.Where(c => c.ProductStatus == ProductStatus.Damaged).Count()
                }).FirstOrDefault();
                return counts ?? new ProductsCountDto();
            }
        }
    }
}
