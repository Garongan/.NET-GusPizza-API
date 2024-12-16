using GusPizza.Domain.Entities;

namespace GusPizza.Application.Services;

public interface IPizzaService
{
    Task<Pizza> GetByIdAsync(Guid id);
    Task<List<Pizza>> GetAllAsync(bool isDeleted);
    Task<Pizza> CreateAsync(string name, decimal price);
    Task<Pizza> UpdateAsync(Guid id, string name, decimal price);
    Task DeleteAsync(Guid id);
}
