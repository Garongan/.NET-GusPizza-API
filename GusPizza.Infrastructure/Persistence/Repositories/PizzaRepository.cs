using GusPizza.Domain.Entities;
using GusPizza.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure.Persistence.Repositories;

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
        pizza.DeletedAt = DateTime.UtcNow;
        appDBContext.Pizzas.Update(pizza);
        await appDBContext.SaveChangesAsync();
    }

    public async Task<List<Pizza>> GetAllAsync(bool isDeleted)
    {
        if (isDeleted) return await appDBContext.Pizzas.Where(p => p.DeletedAt != null).ToListAsync();
        return await appDBContext.Pizzas.Where(p => p.DeletedAt == null).ToListAsync();
    }

    public async Task<Pizza> GetByIdAsync(Guid id)
    {
        return await appDBContext.Pizzas.FindAsync(id) ?? throw new KeyNotFoundException($"Pizza with id {id} not found.");
    }

    public async Task UpdateAsync(Pizza pizza)
    {
        appDBContext.Pizzas.Update(pizza);
        await appDBContext.SaveChangesAsync();
    }
}
