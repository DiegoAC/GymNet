using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using GymNet.Application.Abstractions.Identity;

namespace GymNet.Infrastructure.Firebase.Auth;

internal sealed class FirebaseAuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly FirebaseAuthOptions _options;

    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    public FirebaseAuthService(HttpClient httpClient, FirebaseAuthOptions options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public Task SignOutAsync()
    {
        // Más adelante podremos limpiar tokens persistidos, etc.
        return Task.CompletedTask;
    }

    public Task<AuthResult> RegisterAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var uri =
            $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_options.ApiKey}";

        return SendAuthRequestAsync(uri, email, password, cancellationToken);
    }

    public Task<AuthResult> SignInAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        var uri =
            $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_options.ApiKey}";

        return SendAuthRequestAsync(uri, email, password, cancellationToken);
    }

    private async Task<AuthResult> SendAuthRequestAsync(
        string uri,
        string email,
        string password,
        CancellationToken ct)
    {
        var request = new AuthRequest
        {
            Email = email,
            Password = password,
            ReturnSecureToken = true
        };

        using var response = await _httpClient.PostAsJsonAsync(uri, request, JsonOptions, ct);
        var content = await response.Content.ReadAsStringAsync(ct);

        if (response.IsSuccessStatusCode)
        {
            var payload = JsonSerializer.Deserialize<AuthResponse>(content, JsonOptions);

            if (payload is null ||
                string.IsNullOrWhiteSpace(payload.LocalId) ||
                string.IsNullOrWhiteSpace(payload.Email))
            {
                return AuthResult.Failure("Respuesta de autenticación no válida.");
            }

            var user = new AuthUser(payload.LocalId, payload.Email);
            // Si más adelante quieres, aquí puedes guardar IdToken / RefreshToken.
            return AuthResult.Success(user);
        }

        // Error HTTP: intentamos leer mensaje de Firebase
        try
        {
            var error = JsonSerializer.Deserialize<AuthErrorResponse>(content, JsonOptions);
            var message = error?.Error?.Message ?? "Error de autenticación desconocido.";
            return AuthResult.Failure(message);
        }
        catch
        {
            return AuthResult.Failure("Error de autenticación.");
        }
    }

    // DTOs internos para Firebase Auth REST

    private sealed class AuthRequest
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool ReturnSecureToken { get; set; }
    }

    private sealed class AuthResponse
    {
        public string LocalId { get; set; } = "";
        public string Email { get; set; } = "";
        public string IdToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public string ExpiresIn { get; set; } = "";
    }

    private sealed class AuthErrorResponse
    {
        public ErrorBody? Error { get; set; }
    }

    private sealed class ErrorBody
    {
        public string? Message { get; set; }
    }
}


