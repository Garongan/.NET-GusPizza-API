
using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Domain;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GusPizza.Infrastructure.Services;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository userRepository = repository;
    public async Task AddAdminIfNoExistsAsync()
    {
        var admin = await userRepository.GetByUsernameAsync("admin");
        if (admin == null)
        {
            var newAdmin = new User("admin", HashPassword("password"), Roles.Admin.ToString());
            await userRepository.AddAsync(newAdmin);
        }
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null) return null;

        var hashedPassword = HashPassword(password);
        return hashedPassword == user.PasswordHash ? user : null;
    }

    private static string HashPassword(string password)
    {
        byte[] salt = new byte[16];

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }

    public async Task<UserDtoResponse> AddAsync(string username, string password)
    {
        var newUser = new User(username, HashPassword(password), Roles.User.ToString());
        await userRepository.AddAsync(newUser);
        return new UserDtoResponse(newUser.Id, newUser.Username, newUser.Role, newUser.CreatedAt, newUser.UpdatedAt);
    }

    public async Task<UserDtoResponse> UpdateAsync(Guid id, string username)
    {
        var user = await userRepository.GetByIdAsync(id);
        user.Username = username;
        user.UpdatedAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(user);
        return new UserDtoResponse(user.Id, user.Username, user.Role, user.CreatedAt, user.UpdatedAt);
    }

    public async Task<UserDtoResponse> GetByIdAsync(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);
        return new UserDtoResponse(user.Id, user.Username, user.Role, user.CreatedAt, user.UpdatedAt);
    }

    public async Task<List<UserDtoResponse>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();
        return users.Select(u => new UserDtoResponse(u.Id, u.Username, u.Role, u.CreatedAt, u.UpdatedAt)).ToList();
    }
}
