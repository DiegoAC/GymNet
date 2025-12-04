using System.Net.Http;
using GymNet.Application.Abstractions.Identity;
using GymNet.Infrastructure.Firebase.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Infrastructure.Firebase;

public static class DependencyInjection
{
    public static IServiceCollection AddFirebaseInfra(
        this IServiceCollection services,
        Action<FirebaseAuthOptions> configureAuth)
    {
        // Crear opciones y configurarlas con la acción que pasamos desde MauiProgram
        var options = new FirebaseAuthOptions();
        configureAuth(options);

        // Registrar las opciones como singleton
        services.AddSingleton(options);

        // HttpClient singleton para hablar con Firebase
        services.AddSingleton<HttpClient>();

        // Servicio de autenticación que usa HttpClient y opciones anteriores
        services.AddScoped<IAuthService, FirebaseAuthService>();

        return services;
    }
}



