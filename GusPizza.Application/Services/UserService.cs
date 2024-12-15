
using GusPizza.Application.Services.Interfaces;
using GusPizza.Domain;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;

namespace GusPizza.Application.Services;

public class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository userRepository = repository;
    public async Task AddAdminIfNoExistsAsync()
    {
        var admin = await userRepository.GetByUsernameAsync("admin");
        if (admin == null)
        {
            var newAdmin = new User
            {
                Username = "admin",
                PasswordHash = HashPassword("password"),
                Role = Roles.Admin.ToString()
            };
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
}
