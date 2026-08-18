namespace UserService.Domain;

public record UserCreatedEvent(Guid UserId, string Username, string Name, DateOnly DateOfBirth, DateTime OccurredAtUtc);
