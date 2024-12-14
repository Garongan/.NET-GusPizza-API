using GusPizza.Domain;

namespace GusPizza.Application;

public class PizzaService(IPizzaRepository repository)
{
    private readonly IPizzaRepository pizzaRepository = repository;

    public async Task<List<PizzaDto>> GetAll()
    {
        var pizzas = await pizzaRepository.GetAll();
        return pizzas.Select(pizza => new PizzaDto(pizza.Id, pizza.Name, pizza.Price, pizza.IsAvailable)).ToList();
    }

    public async Task Create(string name, decimal price)
    {
        var pizza = new Pizza(name, price);
        await pizzaRepository.Add(pizza);
    }
}
