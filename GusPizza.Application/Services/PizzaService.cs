using GusPizza.Application.Services.Interfaces;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;

namespace GusPizza.Application.Services;

public class PizzaService(IPizzaRepository repository) : IPizzaService
{
    private readonly IPizzaRepository pizzaRepository = repository;

    public async Task<Pizza> GetByIdAsync(Guid id)
    {
        var pizza = await pizzaRepository.GetByIdAsync(id);
        return pizza;
    }

    public async Task<List<Pizza>> GetAllAsync()
    {
        return await pizzaRepository.GetAllAsync();
    }

    public async Task<Pizza> CreateAsync(string name, decimal price)
    {
        var pizza = new Pizza(name, price);
        await pizzaRepository.AddAsync(pizza);
        return pizza;
    }

    public async Task<Pizza> UpdateAsync(Guid id, string name, decimal price)
    {
        var pizza = await GetByIdAsync(id);
        pizza.Name = name;
        pizza.Price = price;
        pizza.UpdatedAt = DateTime.UtcNow;
        await pizzaRepository.UpdateAsync(pizza);
        return pizza;
    }

    public async Task DeleteAsync(Guid id)
    {
        await pizzaRepository.DeleteAsync(id);
    }
}
