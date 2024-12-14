using GusPizza.Domain;
using Microsoft.EntityFrameworkCore;

namespace GusPizza.Infrastructure;

public class PizzaRepository(AppDBContext appDBContext) : IPizzaRepository
{
    private readonly AppDBContext _appDBContext = appDBContext;

    public async Task Add(Pizza pizza)
    {
        _appDBContext.Pizzas.Add(pizza);
        await _appDBContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var pizza = await GetById(id);
        _appDBContext.Pizzas.Remove(pizza);
        await _appDBContext.SaveChangesAsync();
    }

    public async Task<List<Pizza>> GetAll()
    {
        return await _appDBContext.Pizzas.ToListAsync();
    }

    public async Task<Pizza> GetById(Guid id)
    {
        var pizza = await _appDBContext.Pizzas.FindAsync(id) ?? throw new KeyNotFoundException($"Pizza with id {id} not found.");
        return pizza;
    }

    public async Task Update(Pizza pizza)
    {
        _appDBContext.Pizzas.Update(pizza);
        await _appDBContext.SaveChangesAsync();
    }
}
