namespace UserService.Domain;

public class UserAppService(IUserRepository repository, IEventPublisher eventPublisher)
{
    public async Task<(CreateUserResult Result, User? User)> CreateUserAsync(User user, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(user.Username))
        {
            return (CreateUserResult.UsernameEmpty, null);
        }
        if (string.IsNullOrEmpty(user.Name))
        {
            return (CreateUserResult.NameEmpty, null);
        }
        if (user.DateOfBirth == default)
        {
            return (CreateUserResult.DateOfBirthEmpty, null);
        }
        if (await repository.ExistsAsync(user.Username, ct))
        {
            return (CreateUserResult.UsernameAlreadyExists, null);
        }

        if (!User.IsAtLeast16YearsOld(user.DateOfBirth, DateOnly.FromDateTime(DateTime.UtcNow)))
        {
            return (CreateUserResult.Underage, null);
        }

        await repository.AddAsync(user, ct);
        await eventPublisher.PublishAsync(
            new UserCreatedEvent(user.Id, user.Username, user.Name, user.DateOfBirth, DateTime.UtcNow),
            ct);

        return (CreateUserResult.Created, user);
    }
}
