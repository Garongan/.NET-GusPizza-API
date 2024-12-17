using System;
using System.Diagnostics.Contracts;

namespace GusPizza.Domain.Entities;

public class TransactionDetail(Guid transactionId, Guid pizzaId, int quantity, decimal price)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid TransactionId { get; set; } = transactionId;
    public Guid PizzaId { get; set; } = pizzaId;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;
    public decimal SubTotal => Quantity * Price;
    public Transaction? Transaction { get; set; }
    public Pizza? Pizza { get; set; }

}
