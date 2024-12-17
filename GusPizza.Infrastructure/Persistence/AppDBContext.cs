
using GusPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Persistence;

public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
{
    public required DbSet<Pizza> Pizzas { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Transaction> Transactions { get; set; }
    public required DbSet<TransactionDetail> TransactionDetails { get; set; }
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

        // Entity transaction config
        var transaction = modelBuilder.Entity<Transaction>();
        transaction.HasKey(t => t.Id);
        transaction.Property(t => t.UserId).IsRequired();
        transaction.Property(t => t.TransactionAt).IsRequired();
        transaction.Property(t => t.Total).IsRequired();

        // Entity transaction detail config
        var transactionDetail = modelBuilder.Entity<TransactionDetail>();
        transactionDetail.HasKey(td => td.Id);
        transactionDetail.Property(td => td.TransactionId).IsRequired();
        transactionDetail.Property(td => td.ItemName).IsRequired();
        transactionDetail.Property(td => td.Quantity).IsRequired();
        transactionDetail.Property(td => td.Price).IsRequired();

        // Relation of transaction and transaction detail
        transaction.HasMany(t => t.TransactionDetails).WithOne(td => td.Transaction).HasForeignKey(td => td.TransactionId);

        base.OnModelCreating(modelBuilder);
    }
}
