namespace Product.Domain.Contracts
{       
    public abstract class AuditableEntity : AuditableEntity<Guid>
    {
    }

    public abstract class AuditableEntity<T> : BaseEntity<T>, IAuditableEntity
    {
        public DateTime CreatedOn { get; private set; }

        public DateTime? LastModifiedOn { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }

        protected AuditableEntity()
        {
            CreatedOn = DateTime.UtcNow;
            LastModifiedOn = DateTime.UtcNow;
        }
    }
}
