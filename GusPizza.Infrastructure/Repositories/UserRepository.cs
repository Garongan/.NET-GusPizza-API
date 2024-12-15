using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Repositories;

public class UserRepository(AppDBContext dBContext) : IUserRepository
{
    private readonly AppDBContext appDBContext = dBContext;
    public async Task AddAsync(User user)
    {
        appDBContext.Users.Add(user);
        await appDBContext.SaveChangesAsync();
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await appDBContext.Users.SingleOrDefaultAsync(u => u.Username == username);
    }
}
