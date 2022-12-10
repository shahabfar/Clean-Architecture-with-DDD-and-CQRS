using Ardalis.Specification;
using Product.Application.DTOs;

namespace Product.Application.Features.Products
{
    public class ProductsWithCategorySpec : Specification<Domain.Entities.Product, ProductDto>
    {
        public ProductsWithCategorySpec(GetAllProductsRequest request) : base()
        {
            var query = Query.Include(p => p.Category)
                .Where(p => p.CategoryId.Equals(request.CategoryId!.Value), request.CategoryId.HasValue);

            if (request.PageNumber <= 0)
                request.PageNumber = 1;

            if (request.PageSize <= 0)
                request.PageSize = 10;

            if (request.PageNumber > 1)
                query.Skip((request.PageNumber - 1) * request.PageSize);

            query.Take(request.PageSize);
        }
    }
}
