using System;
using GusPizza.Domain.Entities;

namespace GusPizza.Application.Services.Interfaces;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string username, string password);
    Task AddAdminIfNoExistsAsync();
}
