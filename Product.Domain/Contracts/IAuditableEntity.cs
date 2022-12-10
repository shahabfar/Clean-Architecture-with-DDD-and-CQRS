namespace Product.Domain.Contracts
{
    public interface IAuditableEntity
    {
        public DateTime CreatedOn { get; }

        public DateTime? LastModifiedOn { get; set; }

        public Guid CreatedBy { get; set; }
        public Guid LastModifiedBy { get; set; }
    }
}
