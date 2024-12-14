namespace GusPizza.Application;

public record PizzaDto(Guid Id, string Name, decimal Price, bool IsAvailable);