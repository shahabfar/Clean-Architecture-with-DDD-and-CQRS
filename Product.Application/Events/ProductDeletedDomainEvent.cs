using MediatR;
using Product.Domain.Contracts;

namespace Product.Application.DomainEvents.Events
{
    public class ProductDeletedDomainEvent : DomainEvent, INotification
    {
        public Domain.Entities.Product Product { get; private set; } = default!;
        public ProductDeletedDomainEvent(Domain.Entities.Product product)
        {
            Product = product;
        }

        public class ProductDeletedDomainEventHandler : INotificationHandler<ProductDeletedDomainEvent>
        {
            public ProductDeletedDomainEventHandler()
            {
            }

            public /*async*/ Task Handle(ProductDeletedDomainEvent @event, CancellationToken cancellationToken)
            {
                ///TODO
                return Task.CompletedTask;
            }
        }
    }
}
