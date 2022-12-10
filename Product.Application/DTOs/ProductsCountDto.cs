using Product.Application.Interfaces;

namespace Product.Application.DTOs
{
    public class ProductsCountDto : IDto
    {
        public int Sold { get; set; }
        public int InStock { get; set; }
        public int Damaged { get; set; }
    }
}
