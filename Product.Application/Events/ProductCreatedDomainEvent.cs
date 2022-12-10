using MediatR;
using Product.Domain.Contracts;

namespace Product.Application.Events
{
    public class ProductCreatedDomainEvent : DomainEvent, INotification
    {
        public Domain.Entities.Product Product { get; private set; } = default!;
        public ProductCreatedDomainEvent(Domain.Entities.Product product)
        {
            Product = product;
        }

        public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
        {
            public ProductCreatedDomainEventHandler()
            {
            }

            public /*async*/ Task Handle(ProductCreatedDomainEvent @event, CancellationToken cancellationToken)
            {
                ///TODO
                return Task.CompletedTask;
            }
        }
    }
}
