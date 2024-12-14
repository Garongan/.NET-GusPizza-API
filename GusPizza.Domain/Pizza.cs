using System;

namespace GusPizza.Domain;

public class Pizza
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }

    private Pizza() { }
    public Pizza(string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        IsAvailable = true;
    }
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
