using MediatR;
using Product.Domain.Contracts;

namespace Product.Application.DomainEvents.Events
{
    public class ProductUpdatedDomainEvent : DomainEvent, INotification
    {
        public Domain.Entities.Product Product { get; private set; } = default!;
        public ProductUpdatedDomainEvent(Domain.Entities.Product product)
        {
            Product = product;
        }

        public class ProductUpdatedDomainEventHandler : INotificationHandler<ProductUpdatedDomainEvent>
        {
                    public ProductUpdatedDomainEventHandler()
            {
            }

            public /*async*/ Task Handle(ProductUpdatedDomainEvent @event, CancellationToken cancellationToken)
            {
                ///TODO
                return Task.CompletedTask;
            }}
    }
}
