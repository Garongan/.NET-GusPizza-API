using GusPizza.Domain.Entities;

namespace GusPizza.Application.Services;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task AddAdminIfNoExistsAsync();
    Task<User> AddAsync(string username, string password);
    Task UpdateAsync(Guid id, string username);
    Task<User> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
}
