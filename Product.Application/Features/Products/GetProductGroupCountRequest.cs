using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.Features.Products
{
    public record GetProductGroupCountRequest(ProductStatus ProductStatus) : IRequest<int>
    {
        public class GetProductGroupCountRequestHandler : IRequestHandler<GetProductGroupCountRequest, int>
        {
            private readonly IRepository<Domain.Entities.Product> _repository;

            public GetProductGroupCountRequestHandler(IRepository<Domain.Entities.Product> repository) => _repository = repository;

            public async Task<int> Handle(GetProductGroupCountRequest request, CancellationToken cancellationToken)
            {
                var spec = new ProductGroupSpec(request);
                return await _repository.CountAsync(spec, cancellationToken: cancellationToken);
            }
        }
    }
}
