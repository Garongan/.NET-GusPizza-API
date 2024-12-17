using System;
using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;

namespace GusPizza.Infrastructure.Services;

public class TransactionService(ITransactionRepository repository) : ITransactionService
{
    private readonly ITransactionRepository transactionRepository = repository;
    public async Task<TransactionDtoResponse> AddAsync(Guid userId, TransactionDtoRequest request)
    {
        var transaction = new Transaction(userId, DateTime.UtcNow);

        request.Details.ForEach(td =>
        {
            var transactionDetail = new TransactionDetail(transaction.Id, td.ItemName, td.Quantity, td.Price);
            transaction.TransactionDetails.Add(transactionDetail);
            transaction.Total += transactionDetail.SubTotal;
        });

        await transactionRepository.AddAsync(transaction);
        var transactionDetails = transaction.TransactionDetails.Select(td => new TransactionDetailDtoResponse(td.Id, td.ItemName, td.Quantity, td.Price, td.SubTotal)).ToList();
        return new TransactionDtoResponse(transaction.Id, transaction.UserId, transaction.TransactionAt, transaction.Total, transactionDetails);
    }

    public async Task<List<TransactionDtoResponse>> GetAllAsync()
    {
        var transactions = await transactionRepository.GetAllAsync();
        return transactions.Select(t => new TransactionDtoResponse(
            t.Id,
            t.UserId,
            t.TransactionAt,
            t.Total,
            t.TransactionDetails.Select(
                td => new TransactionDetailDtoResponse(td.Id, td.ItemName, td.Quantity, td.Price, td.SubTotal)
            ).ToList()
            )).ToList();
    }

    public async Task<TransactionDtoResponse> GetByIdAsync(Guid id)
    {
        var transaction = await transactionRepository.GetByIdAsync(id);
        var transactionDetails = transaction.TransactionDetails.Select(td => new TransactionDetailDtoResponse(td.Id, td.ItemName, td.Quantity, td.Price, td.SubTotal)).ToList();
        return new TransactionDtoResponse(transaction.Id, transaction.UserId, transaction.TransactionAt, transaction.Total, transactionDetails);
    }

    public async Task<List<TransactionDtoResponse>> GetByUserIdAsync(Guid userId)
    {
        var transactions = await transactionRepository.GetByUserIdAsync(userId);
        return transactions.Select(t => new TransactionDtoResponse(
            t.Id,
            t.UserId,
            t.TransactionAt,
            t.Total,
            t.TransactionDetails.Select(
                td => new TransactionDetailDtoResponse(td.Id, td.ItemName, td.Quantity, td.Price, td.SubTotal)
            ).ToList()
            )).ToList();
    }
}
