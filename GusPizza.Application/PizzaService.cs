
using GusPizza.Domain;

namespace GusPizza.Application;

public class PizzaService(IPizzaRepository repository)
{
    private readonly IPizzaRepository pizzaRepository = repository;

    public async Task<Pizza> GetById(Guid id)
    {
        var pizza = await pizzaRepository.GetById(id);
        return pizza;
    }
    public async Task<List<Pizza>> GetAll()
    {
        return await pizzaRepository.GetAll();
    }

    public async Task<Pizza> Create(string name, decimal price)
    {
        var pizza = new Pizza(name, price);
        await pizzaRepository.Add(pizza);
        return pizza;
    }

    public async Task<Pizza> Update(Guid id, string name, decimal price)
    {
        var pizza = await GetById(id);
        pizza.Name = name;
        pizza.Price = price;
        await pizzaRepository.Update(pizza);
        return pizza;
    }

    public async Task Delete(Guid id)
    {
        await pizzaRepository.Delete(id);
    }
}
