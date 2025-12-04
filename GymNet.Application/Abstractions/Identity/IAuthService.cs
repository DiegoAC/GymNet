namespace GymNet.Application.Abstractions.Identity;

public interface IAuthService
{
    Task<AuthResult> SignInAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task<AuthResult> RegisterAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);

    Task SignOutAsync();
}

