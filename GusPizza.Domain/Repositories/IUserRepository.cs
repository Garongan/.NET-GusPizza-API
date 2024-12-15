using GusPizza.Domain.Entities;

namespace GusPizza.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}
