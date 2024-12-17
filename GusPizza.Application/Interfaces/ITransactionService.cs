using System;
using System.Transactions;
using GusPizza.Application.Dto;

namespace GusPizza.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionDtoResponse> AddAsync(Guid userId, TransactionDtoRequest request);
    Task<List<TransactionDtoResponse>> GetAllAsync();
    Task<TransactionDtoResponse> GetByIdAsync(Guid id);
    Task<List<TransactionDtoResponse>> GetByUserIdAsync(Guid userId);
}
