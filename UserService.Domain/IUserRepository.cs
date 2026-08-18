namespace UserService.Domain;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username, CancellationToken ct);
    Task<User[]> GetAllAsync(CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task<bool> ExistsAsync(string username, CancellationToken ct);
}
