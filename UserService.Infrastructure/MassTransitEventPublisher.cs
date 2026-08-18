using MassTransit;
using UserService.Domain;

namespace UserService.Infrastructure;

public class MassTransitEventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher
{
    public Task PublishAsync<T>(T @event, CancellationToken ct) where T : class
        => publishEndpoint.Publish(@event, ct);
}
