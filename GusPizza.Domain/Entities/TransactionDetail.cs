using System;

namespace GusPizza.Domain.Entities;

public class TransactionDetail(Guid transactionId, string itemName, int quantity, decimal price)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TransactionId { get; set; } = transactionId;
    public string ItemName { get; set; } = itemName;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;
    public decimal SubTotal => Quantity * Price;
    public Transaction? Transaction { get; set; }
}
