using GusPizza.Application.Dto;

namespace GusPizza.Application.Interfaces;

public interface IPizzaService
{
    Task<PizzaDtoResponse> GetByIdAsync(Guid id);
    Task<List<PizzaDtoResponse>> GetAllAsync(bool isDeleted);
    Task<PizzaDtoResponse> CreateAsync(string name, decimal price);
    Task<PizzaDtoResponse> UpdateAsync(Guid id, string name, decimal price);
    Task DeleteAsync(Guid id);
}
