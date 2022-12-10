using Product.Application.Interfaces;
using Product.Domain.Enums;

namespace Product.Application.DTOs
{
    public class ProductDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Barcode { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsWeighted { get; set; }
        public ProductStatus ProductStatus { get; private set; }

        public CategoryDto Category { get; set; } = default!;
    }
}
