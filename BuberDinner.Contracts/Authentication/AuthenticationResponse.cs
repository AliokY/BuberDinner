namespace BuberDinner.Contracts.Authentication;

public record AuthenticationResponse (
    Guid Id,
    string LastName,
    string Email,
    string Password,
    string Token);