using System;

namespace GusPizza.Domain.Entities;

public class Transaction(Guid userId, DateTime transactionAt)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = userId;
    public DateTime TransactionAt { get; set; } = transactionAt;
    public decimal Total { get; set; } = 0;
    public List<TransactionDetail> TransactionDetails {get; set;} = [];
}
