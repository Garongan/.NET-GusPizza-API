
using GusPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Persistence;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public required DbSet<Pizza> Pizzas { get; set; }
    public required DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity pizza config
        var pizza = modelBuilder.Entity<Pizza>();
        pizza.HasKey(p => p.Id);
        pizza.HasIndex(p => p.Name).IsUnique();
        pizza.Property(p => p.Name).IsRequired();
        pizza.Property(p => p.Price).IsRequired();

        // Entity user config
        var user = modelBuilder.Entity<User>();
        user.HasKey(u => u.Id);
        user.HasIndex(u => u.Username).IsUnique();
        user.Property(u => u.Username).IsRequired().HasMaxLength(100);
        user.Property(u => u.PasswordHash).IsRequired();
        user.Property(u => u.Role).IsRequired().HasMaxLength(50);
        
        base.OnModelCreating(modelBuilder);
    }
}
