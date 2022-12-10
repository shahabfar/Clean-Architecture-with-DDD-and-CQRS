using Ardalis.Specification;

namespace Product.Application.Features.Products
{
    public class ProductGroupSpec : Specification<Domain.Entities.Product>
    {
        public ProductGroupSpec(GetProductGroupCountRequest request) : base()
        {
            Query.Include(p => p.Category)
                .Where(p => p.ProductStatus == request.ProductStatus);
        }
    }
}
