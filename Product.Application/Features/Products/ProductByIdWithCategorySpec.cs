using Ardalis.Specification;
using Product.Application.DTOs;

namespace Product.Application.Features.Products
{
    internal class ProductByIdWithCategorySpec : Specification<Domain.Entities.Product, ProductDto>, ISingleResultSpecification
    {
        public ProductByIdWithCategorySpec(int id)
        {
            Query.Where(h => h.Id == id).Include(p => p.Category);
        }
    }
}
