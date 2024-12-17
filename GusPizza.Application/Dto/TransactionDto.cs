using GusPizza.Domain.Entities;

namespace GusPizza.Application.Dto;

public record TransactionDtoRequest(List<TransactionDetailDtoRequest> Details);
public record TransactionDtoResponse(
    Guid Id,
    Guid UserId,
    DateTime TransactionAt,
    decimal Total,
    List<TransactionDetailDtoResponse> TransactionDetails
);
public record TransactionDetailDtoRequest(Guid PizzaId, int Quantity, decimal Price);
public record PizzaDtoInsideTransactionResponse(Guid Id, string Name);
public record TransactionDetailDtoResponse(
    Guid Id,
    PizzaDtoInsideTransactionResponse Pizza,
    int Quantity,
    decimal Price,
    decimal SubTotal
);
