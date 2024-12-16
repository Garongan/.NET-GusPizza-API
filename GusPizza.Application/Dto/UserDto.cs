namespace GusPizza.Application.Dto;

public record LoginDtoRequest(string Username, string Password);
public record LoginDtoResponse(string Token, string Role);
public record UserDtoRequest(string Username, string Password);
public record UpdateUserDtoRequest(string Username);
public record UserDtoResponse(
    Guid Id,
    string Username,
    string Role,
    DateTime CreatedAt,
    DateTime UpdatedAt
);