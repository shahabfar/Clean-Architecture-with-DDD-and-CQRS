using Product.Application.Interfaces;

namespace Product.Application.DTOs
{
    public class CategoryDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}
