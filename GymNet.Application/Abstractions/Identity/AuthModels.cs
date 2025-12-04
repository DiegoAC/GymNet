namespace GymNet.Application.Abstractions.Identity;

public sealed record AuthUser(string UserId, string Email);

public sealed record AuthResult(bool IsSuccess, AuthUser? User, string? Error)
{
    public static AuthResult Success(AuthUser user) => new(true, user, null);
    public static AuthResult Failure(string error) => new(false, null, error);
}


