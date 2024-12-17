using System;
using GusPizza.Domain.Entities;

namespace GusPizza.Domain.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetAllAsync();
    Task<Transaction> GetByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetByUserIdAsync(Guid userId);
}
