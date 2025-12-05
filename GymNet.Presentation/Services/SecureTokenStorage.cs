using GymNet.Application.Abstractions.Services;

namespace GymNet.Presentation.Services;

/// <summary>
/// Implementación de almacenamiento seguro de tokens usando SecureStorage de MAUI.
/// Los tokens se guardan encriptados en iOS Keychain y Android KeyStore.
/// </summary>
public sealed class SecureTokenStorage : ITokenStorage
{
    private const string IdTokenKey = "gymnet_id_token";
    private const string RefreshTokenKey = "gymnet_refresh_token";
    private const string UserIdKey = "gymnet_user_id";
    private const string EmailKey = "gymnet_email";

    public async Task SaveTokenAsync(string idToken, string refreshToken, string userId, string email)
    {
        try
        {
            await SecureStorage.SetAsync(IdTokenKey, idToken);
            await SecureStorage.SetAsync(RefreshTokenKey, refreshToken);
            await SecureStorage.SetAsync(UserIdKey, userId);
            await SecureStorage.SetAsync(EmailKey, email);
        }
        catch (Exception ex)
        {
            // Log error pero no fallar - en producción considera logging
            System.Diagnostics.Debug.WriteLine($"Error guardando tokens: {ex.Message}");
            throw;
        }
    }

    public async Task<(string idToken, string refreshToken, string userId, string email)?> GetTokensAsync()
    {
        try
        {
            var idToken = await SecureStorage.GetAsync(IdTokenKey);
            var refreshToken = await SecureStorage.GetAsync(RefreshTokenKey);
            var userId = await SecureStorage.GetAsync(UserIdKey);
            var email = await SecureStorage.GetAsync(EmailKey);

            if (string.IsNullOrWhiteSpace(idToken) ||
                string.IsNullOrWhiteSpace(refreshToken) ||
                string.IsNullOrWhiteSpace(userId) ||
                string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            return (idToken, refreshToken, userId, email);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error recuperando tokens: {ex.Message}");
            return null;
        }
    }

    public async Task ClearTokensAsync()
    {
        try
        {
            SecureStorage.Remove(IdTokenKey);
            SecureStorage.Remove(RefreshTokenKey);
            SecureStorage.Remove(UserIdKey);
            SecureStorage.Remove(EmailKey);
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error limpiando tokens: {ex.Message}");
        }
    }

    public async Task<bool> HasSessionAsync()
    {
        var tokens = await GetTokensAsync();
        return tokens.HasValue;
    }
}
