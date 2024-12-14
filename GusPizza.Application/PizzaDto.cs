namespace GusPizza.Application;

public record PizzaDtoRequest(string Name, decimal Price);
public record PizzaDtoResponse(Guid Id, string Name, decimal Price, bool IsAvailable);