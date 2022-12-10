using MediatR;
using Product.Domain.Contracts;

namespace Product.Application.Events
{
    public class ProductStatusUpdatedDomainEvent : DomainEvent, INotification
    {
        public Domain.Entities.Product Product { get; private set; } = default!;
        public ProductStatusUpdatedDomainEvent(Domain.Entities.Product product)
        {
            Product = product;
        }

        public class ProductStatusUpdatedDomainEventHandler : INotificationHandler<ProductStatusUpdatedDomainEvent>
        {
            public ProductStatusUpdatedDomainEventHandler()
            {
            }

            public /*async*/ Task Handle(ProductStatusUpdatedDomainEvent @event, CancellationToken cancellationToken)
            {
                ///TODO
                // we can do appropriate implementation if the product status is changed to Sold

                return Task.CompletedTask;
            }
        }
    }

}
