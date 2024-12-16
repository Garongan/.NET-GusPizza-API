using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDBContext dBContext) : IUserRepository
{
    private readonly AppDBContext appDBContext = dBContext;
    public async Task AddAsync(User user)
    {
        appDBContext.Users.Add(user);
        await appDBContext.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await appDBContext.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await appDBContext.Users.FindAsync(id) ?? throw new KeyNotFoundException($"User with {id} not found");
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await appDBContext.Users.SingleOrDefaultAsync(u => u.Username == username);
    }

    public async Task UpdateAsync(User user)
    {
        appDBContext.Users.Update(user);
        await appDBContext.SaveChangesAsync();
    }
}
