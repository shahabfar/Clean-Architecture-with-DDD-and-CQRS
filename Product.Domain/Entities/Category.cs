using Product.Domain.Contracts;

namespace Product.Domain.Entities
{
    public class Category : BaseEntity<int>, IAggregateRoot
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; } = default;

        public Category(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public Category(int Id, string name, string? description)
        {
            this.Id = Id;
            Name = name;
            Description = description;
        }

        public Category Update(string? name, string? description)
        {
            if (name is not null && Name?.Equals(name) is not true) Name = name;
            if (description is not null && Description?.Equals(description) is not true) Description = description;
            return this;
        }

    }
}
