using GymNet.Application.Abstractions.Services;
using GymNet.Application.Auth;

namespace GymNet.Infrastructure.Firebase.Auth;

// De momento simula Firebase: siempre “loguea” con un usuario de prueba.
public sealed class FakeAuthService : IAuthService
{
    public Task<AuthResult> SignInWithEmailPasswordAsync(string email, string password, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Email y contraseña obligatorios.");

        // Aquí luego pondremos la llamada real a Firebase Auth
        var result = new AuthResult(
            UserId: "dev-user",
            Email: email,
            DisplayName: "Dev User",
            IdToken: "fake-token"
        );

        return Task.FromResult(result);
    }

    public Task<AuthResult> RegisterWithEmailPasswordAsync(string email, string password, CancellationToken ct)
        => SignInWithEmailPasswordAsync(email, password, ct);
}

