using Product.Domain.Contracts;
using Product.Domain.Enums;

namespace Product.Domain.Entities
{
    public class Product : AuditableEntity<int>, IAggregateRoot
    {
        public string Name { get; private set; } = default!;
        public string Barcode { get; private set; }
        public string? Description { get; private set; }
        public bool IsWeighted { get; private set; }
        public ProductStatus ProductStatus { get; private set; }
        public int CategoryId { get; private set; }

        public Category Category { get; private set; } = default!;

        public Product(string name, string barcode, string? description, bool isWeighted, ProductStatus productStatus, int categoryId)
        {
            Name = name;
            Barcode = barcode;
            Description = description;
            IsWeighted = isWeighted;
            CategoryId = categoryId;
            ProductStatus = productStatus;
            CategoryId = categoryId;
        }

        public Product(int Id, string name, string barcode, string? description, bool isWeighted, ProductStatus productStatus, int categoryId)
        {
            this.Id = Id;
            Name = name;
            Barcode = barcode;
            Description = description;
            IsWeighted = isWeighted;
            CategoryId = categoryId;
            ProductStatus = productStatus;
            CategoryId = categoryId;
        }

        public Product Update(string? name, string? barcode, string? description, bool isWieghted, ProductStatus? status, int? categoryId)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (barcode is not null && Barcode?.Equals(barcode) is not true) Barcode = barcode;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            if (isWieghted != IsWeighted) IsWeighted = isWieghted;
            if (categoryId.HasValue && !CategoryId.Equals(categoryId.Value)) CategoryId = categoryId.Value;
            if (status is not null) ProductStatus = (ProductStatus)status;
            return this;
        }

        public Product ChangeProductStatus(ProductStatus status)
        {
            ProductStatus = status;
            return this;
        }
    }
}
