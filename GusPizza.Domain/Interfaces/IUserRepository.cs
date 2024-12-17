using GusPizza.Domain.Entities;

namespace GusPizza.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task<User> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task UpdateAsync(User user);
}
