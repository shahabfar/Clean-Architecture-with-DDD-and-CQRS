using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Contracts
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; } = new();
    }

    public abstract class BaseEntity : BaseEntity<Guid> // default Id type
    {
        protected BaseEntity() => Id = Guid.NewGuid();
    }
}
