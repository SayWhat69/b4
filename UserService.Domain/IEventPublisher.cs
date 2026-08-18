namespace UserService.Domain;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event, CancellationToken ct) where T : class;
}
