using MediatR;
using Microsoft.Extensions.Logging;
using Product.Application.Interfaces;
using Product.Domain.Contracts;

namespace Product.Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly ILogger<EventPublisher> _logger;
        private readonly IPublisher _mediator;

        public EventPublisher(ILogger<EventPublisher> logger, IPublisher mediator) =>
            (_logger, _mediator) = (logger, mediator);

        public Task PublishAsync(DomainEvent @event)
        {
            _logger.LogInformation("Publishing Event : {event}", @event.GetType().Name);
            var pub = _mediator.Publish((INotification)@event);
            return pub;
        }
    }
}
