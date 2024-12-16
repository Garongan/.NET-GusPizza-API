
using GusPizza.Application.Services;
using GusPizza.Domain;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;
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

    public async Task<User> AddAsync(string username, string password)
    {
        var newUser = new User(username, HashPassword(password), Roles.User.ToString());
        await userRepository.AddAsync(newUser);
        return newUser;
    }

    public async Task<User> UpdateAsync(Guid id, string username)
    {
        var user = await GetByIdAsync(id);
        user.Username = username;
        user.UpdatedAt = DateTime.UtcNow;
        await userRepository.UpdateAsync(user);
        return user;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await userRepository.GetByIdAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await userRepository.GetAllAsync();
    }
}
