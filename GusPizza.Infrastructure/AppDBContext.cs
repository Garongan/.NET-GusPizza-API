
using GusPizza.Domain;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public required DbSet<Pizza> Pizzas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Pizza>().HasKey(p => p.Id);
    }
}
