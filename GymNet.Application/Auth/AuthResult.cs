namespace GymNet.Application.Auth;

public sealed record AuthResult(
    string UserId,
    string Email,
    string? DisplayName,
    string IdToken
);
