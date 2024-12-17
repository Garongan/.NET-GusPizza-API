using System;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Persistence.Repositories;

public class TransactionRepository(AppDBContext dBContext) : ITransactionRepository
{
    private readonly AppDBContext appDBContext = dBContext;
    public async Task AddAsync(Transaction transaction)
    {
        await appDBContext.Transactions.AddAsync(transaction);
        await appDBContext.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetAllAsync()
    {
        return await appDBContext.Transactions.ToListAsync();
    }

    public async Task<Transaction> GetByIdAsync(Guid id)
    {
        return await appDBContext.Transactions.Include(t => t.TransactionDetails).FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("Transaction not found");
    }

    public async Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId)
    {
        return await appDBContext.Transactions.Where(t => t.UserId == userId).Include(t => t.TransactionDetails).ToListAsync();
    }
}
