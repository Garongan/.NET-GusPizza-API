using GusPizza.Domain.Entities;

namespace GusPizza.Application.Services.Interfaces;

public interface IPizzaService
{
    Task<Pizza> GetByIdAsync(Guid id);
    Task<List<Pizza>> GetAllAsync();
    Task<Pizza> CreateAsync(string name, decimal price);
    Task<Pizza> UpdateAsync(Guid id, string name, decimal price);
    Task DeleteAsync(Guid id);
}
