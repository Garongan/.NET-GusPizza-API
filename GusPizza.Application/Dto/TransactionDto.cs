using GusPizza.Domain.Entities;

namespace GusPizza.Application.Dto;

public record TransactionDtoRequest(List<TransactionDetail> Details);
public record TransactionDtoResponse(
    Guid Id,
    Guid UserId,
    DateTime TransactionAt,
    decimal Total,
    List<TransactionDetailDtoResponse> TransactionDetails
);
public record TransactionDetailDtoRequest(string ItemName, int Quantity, decimal Price);
public record TransactionDetailDtoResponse(
    Guid Id,
    string ItemName,
    int Quantity,
    decimal Price,
    decimal SubTotal
);