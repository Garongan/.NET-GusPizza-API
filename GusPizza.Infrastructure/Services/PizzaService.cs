using GusPizza.Application.Dto;
using GusPizza.Application.Interfaces;
using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;

namespace GusPizza.Infrastructure.Services;

public class PizzaService(IPizzaRepository repository) : IPizzaService
{
    private readonly IPizzaRepository pizzaRepository = repository;

    public async Task<PizzaDtoResponse> GetByIdAsync(Guid id)
    {
        var pizza = await pizzaRepository.GetByIdAsync(id);
        return new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt);
    }

    public async Task<List<PizzaDtoResponse>> GetAllAsync(bool isDeleted)
    {
        var pizzas = await pizzaRepository.GetAllAsync(isDeleted);
        return pizzas.Select(p => new PizzaDtoResponse(p.Id, p.Name, p.Price, p.IsAvailable, p.CreatedAt, p.UpdatedAt, p.DeletedAt)).ToList();
    }

    public async Task<PizzaDtoResponse> CreateAsync(string name, decimal price)
    {
        var pizza = new Pizza(name, price);
        await pizzaRepository.AddAsync(pizza);
        return new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt);
    }

    public async Task<PizzaDtoResponse> UpdateAsync(Guid id, string name, decimal price)
    {
        var pizza = await pizzaRepository.GetByIdAsync(id);
        pizza.Name = name;
        pizza.Price = price;
        pizza.UpdatedAt = DateTime.UtcNow;
        await pizzaRepository.UpdateAsync(pizza);
        return new PizzaDtoResponse(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable, pizza.CreatedAt, pizza.UpdatedAt, pizza.DeletedAt);
    }

    public async Task DeleteAsync(Guid id)
    {
        await pizzaRepository.DeleteAsync(id);
    }
}
