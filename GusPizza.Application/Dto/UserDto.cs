namespace GusPizza.Application.Dto;

public record LoginDtoRequest(string Username, string Password);
public record LoginDtoResponse(string Token, string Role);