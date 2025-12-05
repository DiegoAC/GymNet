namespace GymNet.Application.Abstractions.Services;

/// <summary>
/// Servicio para almacenar y recuperar tokens de autenticación de forma segura.
/// </summary>
public interface ITokenStorage
{
    /// <summary>
    /// Guarda los tokens de autenticación de forma segura.
    /// </summary>
    Task SaveTokenAsync(string idToken, string refreshToken, string userId, string email);

    /// <summary>
    /// Recupera los tokens almacenados.
    /// </summary>
    /// <returns>Tupla con idToken, refreshToken, userId y email. Null si no hay tokens.</returns>
    Task<(string idToken, string refreshToken, string userId, string email)?> GetTokensAsync();

    /// <summary>
    /// Elimina todos los tokens almacenados (logout).
    /// </summary>
    Task ClearTokensAsync();

    /// <summary>
    /// Verifica si hay una sesión guardada.
    /// </summary>
    Task<bool> HasSessionAsync();
}
