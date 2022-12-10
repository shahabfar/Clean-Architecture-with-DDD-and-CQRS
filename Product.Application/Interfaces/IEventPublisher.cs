using Product.Domain.Contracts;

namespace Product.Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync(DomainEvent @event);
    }
}