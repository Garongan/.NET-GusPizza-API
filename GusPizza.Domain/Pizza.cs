using System;

namespace GusPizza.Domain;

public class Pizza(string name, decimal price)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public bool IsAvailable { get; set; } = true;

    public void Update(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
    public void ToggleAvailability()
    {
        IsAvailable = !IsAvailable;
    }
}
