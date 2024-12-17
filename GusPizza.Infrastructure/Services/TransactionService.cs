using System;
using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;

namespace GusPizza.Infrastructure.Services;

public class TransactionService(ITransactionRepository transactionRepository, IPizzaRepository pizzaRepository) : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IPizzaRepository _pizzaRepository = pizzaRepository;
    public async Task<TransactionDtoResponse> AddAsync(Guid userId, TransactionDtoRequest request)
    {
        var transaction = new Transaction(userId, DateTime.UtcNow);

        request.Details.ForEach(async td =>
        {
            var pizza = await _pizzaRepository.GetByIdAsync(td.PizzaId);
            var transactionDetail = new TransactionDetail(transaction.Id, pizza.Id, td.Quantity, pizza.Price);
            transaction.TransactionDetails.Add(transactionDetail);
            transaction.Total += transactionDetail.SubTotal;
        });

        await _transactionRepository.AddAsync(transaction);
        var transactionDetails = transaction.TransactionDetails.Select(
            td => new TransactionDetailDtoResponse(
                td.Id,
                new PizzaDtoInsideTransactionResponse(
                    td.Pizza?.Id ?? throw new KeyNotFoundException("Pizza not found"),
                    td.Pizza.Name),
                td.Quantity,
                td.Price,
                td.SubTotal)
            ).ToList();
        return new TransactionDtoResponse(transaction.Id, transaction.UserId, transaction.TransactionAt, transaction.Total, transactionDetails);
    }

    public async Task<List<TransactionDtoResponse>> GetAllAsync()
    {
        var transactions = await _transactionRepository.GetAllAsync();
        return transactions.Select(t => new TransactionDtoResponse(
            t.Id,
            t.UserId,
            t.TransactionAt,
            t.Total,
            t.TransactionDetails.Select(
                td => new TransactionDetailDtoResponse(
                    td.Id,
                    new PizzaDtoInsideTransactionResponse(td.Pizza?.Id ?? throw new KeyNotFoundException("Pizza not found"), td.Pizza.Name),
                    td.Quantity,
                    td.Price,
                    td.SubTotal)
            ).ToList()
            )).ToList();
    }

    public async Task<TransactionDtoResponse> GetByIdAsync(Guid id)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id);
        var transactionDetails = transaction.TransactionDetails.Select(
            td => new TransactionDetailDtoResponse(
                td.Id, 
                new PizzaDtoInsideTransactionResponse(
                    td.Pizza?.Id ?? throw new KeyNotFoundException("Pizza not found"), 
                    td.Pizza.Name),
                td.Quantity, 
                td.Price, 
                td.SubTotal)
            ).ToList();
        return new TransactionDtoResponse(transaction.Id, transaction.UserId, transaction.TransactionAt, transaction.Total, transactionDetails);
    }

    public async Task<List<TransactionDtoResponse>> GetByUserIdAsync(Guid userId)
    {
        var transactions = await _transactionRepository.GetByUserIdAsync(userId);
        return transactions.Select(t => new TransactionDtoResponse(
            t.Id,
            t.UserId,
            t.TransactionAt,
            t.Total,
            t.TransactionDetails.Select(
                td => new TransactionDetailDtoResponse(td.Id, new PizzaDtoInsideTransactionResponse(
                    td.Pizza?.Id ?? throw new KeyNotFoundException("Pizza not found"), 
                    td.Pizza.Name), 
                td.Quantity, 
                td.Price, 
                td.SubTotal)
            ).ToList()
            )).ToList();
    }
}
