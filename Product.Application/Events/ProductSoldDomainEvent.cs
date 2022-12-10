using MediatR;
using Product.Domain.Contracts;

namespace Product.Application.Events
{
    public class ProductSoldDomainEvent : DomainEvent, INotification
    {
        public Domain.Entities.Product Product { get; private set; } = default!;
        public ProductSoldDomainEvent(Domain.Entities.Product product)
        {
            Product = product;
        }

        public class ProductSoldDomainEventHandler : INotificationHandler<ProductSoldDomainEvent>
        {
            public ProductSoldDomainEventHandler()
            {
            }

            public /*async*/ Task Handle(ProductSoldDomainEvent @event, CancellationToken cancellationToken)
            {
                ///TODO
                return Task.CompletedTask;
            }
        }
    }

}
