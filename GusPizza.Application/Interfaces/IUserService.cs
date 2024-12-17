using GusPizza.Application.Dto;
using GusPizza.Domain.Entities;

namespace GusPizza.Application.Interfaces;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task AddAdminIfNoExistsAsync();
    Task<UserDtoResponse> AddAsync(string username, string password);
    Task<UserDtoResponse> UpdateAsync(Guid id, string username);
    Task<UserDtoResponse> GetByIdAsync(Guid id);
    Task<List<UserDtoResponse>> GetAllAsync();
}
