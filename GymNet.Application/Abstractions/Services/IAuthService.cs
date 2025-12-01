using GymNet.Application.Auth;

namespace GymNet.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AuthResult> SignInWithEmailPasswordAsync(string email, string password, CancellationToken ct);
    Task<AuthResult> RegisterWithEmailPasswordAsync(string email, string password, CancellationToken ct);
}

