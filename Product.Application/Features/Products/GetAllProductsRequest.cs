using MediatR;
using Product.Application.DTOs;
using Product.Application.Interfaces;
using Product.Application.Paginations;

namespace Product.Application.Features.Products
{
    public class GetAllProductsRequest : Pagination, IRequest<PaginationResponse<ProductDto>>
    {
        public int? CategoryId { get; set; }

        public class GetAllProductsRequestHandler : IRequestHandler<GetAllProductsRequest, PaginationResponse<ProductDto>>
        {
            private readonly IReadRepository<Domain.Entities.Product> _repository;

            public GetAllProductsRequestHandler(IReadRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<PaginationResponse<ProductDto>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
            {
                var spec = new ProductsWithCategorySpec(request);
                return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
            }
        }
    }
}
