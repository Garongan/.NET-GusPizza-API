using GusPizza.Domain.Entities;
using GusPizza.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Repositories;

public class PizzaRepository(AppDBContext dBContext) : IPizzaRepository
{
    private readonly AppDBContext appDBContext = dBContext;

    public async Task AddAsync(Pizza pizza)
    {
        appDBContext.Pizzas.Add(pizza);
        await appDBContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var pizza = await GetByIdAsync(id);
        appDBContext.Pizzas.Remove(pizza);
        await appDBContext.SaveChangesAsync();
    }

    public async Task<List<Pizza>> GetAllAsync()
    {
        return await appDBContext.Pizzas.ToListAsync();
    }

    public async Task<Pizza> GetByIdAsync(Guid id)
    {
        var pizza = await appDBContext.Pizzas.FindAsync(id) ?? throw new KeyNotFoundException($"Pizza with id {id} not found.");
        return pizza;
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        appDBContext.Pizzas.Update(pizza);
        await appDBContext.SaveChangesAsync();
    }
}
