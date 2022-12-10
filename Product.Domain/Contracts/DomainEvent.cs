namespace Product.Domain.Contracts
{
    public abstract class DomainEvent
    {
        public DateTime TriggeredOn { get; protected set; } = DateTime.UtcNow;
    }
}
