
using GusPizza.Domain.Entities;

namespace GusPizza.Domain.Repositories;

public interface IPizzaRepository
{
    Task<Pizza> GetByIdAsync(Guid id);
    Task<List<Pizza>> GetAllAsync(bool isDeleted);
    Task AddAsync(Pizza pizza);
    Task UpdateAsync(Pizza pizza);
    Task DeleteAsync(Guid id);
}
