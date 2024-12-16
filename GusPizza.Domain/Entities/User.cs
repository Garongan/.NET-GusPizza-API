namespace GusPizza.Domain.Entities;

public class User(string username, string passwordHash, string role)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = username;
    public string PasswordHash { get; set; } = passwordHash;
    public string Role { get; set; } = role;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
