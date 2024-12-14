namespace GusPizza.Domain;

public interface IPizzaRepository
{
    Task<Pizza> GetById(Guid id);
    Task<List<Pizza>> GetAll();
    Task Add(Pizza pizza);
    Task Update(Pizza pizza);
    Task Delete(Guid id);
}
