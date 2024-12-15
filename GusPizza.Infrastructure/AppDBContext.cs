
using GusPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public required DbSet<Pizza> Pizzas { get; set; }
    public required DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity pizza config
        modelBuilder.Entity<Pizza>().HasKey(p => p.Id);

        // Entity user config
        var user = modelBuilder.Entity<User>();
        user.HasKey(u => u.Id);
        user.Property(u => u.Username).IsRequired().HasMaxLength(100);
        user.Property(u => u.PasswordHash).IsRequired();
        user.Property(u => u.Role).IsRequired().HasMaxLength(50);
        
        base.OnModelCreating(modelBuilder);
    }
}
