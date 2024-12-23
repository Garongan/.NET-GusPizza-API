namespace GusPizza.Application.Dto;

public record PizzaDtoRequest(string Name, decimal Price);
public record PizzaDtoResponse(
    Guid Id,
    string Name,
    decimal Price,
    bool IsAvailable,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    DateTime? DeletedAt
);