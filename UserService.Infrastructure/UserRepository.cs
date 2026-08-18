using Microsoft.EntityFrameworkCore;
using UserService.Domain;

namespace UserService.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken ct)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Username == username, ct);
    }

    public async Task<User[]> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Users.ToArrayAsync(ct);
    }

    public async Task AddAsync(User user, CancellationToken ct)
    {
        await _dbContext.Users.AddAsync(user, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken ct)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username, ct);
    }
}
