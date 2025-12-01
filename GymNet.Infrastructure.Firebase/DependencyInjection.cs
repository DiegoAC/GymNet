using GymNet.Application.Abstractions.Persistence;
using GymNet.Application.Abstractions.Services;
using GymNet.Infrastructure.Firebase.Auth;
using GymNet.Infrastructure.Firebase.Persistence;
using GymNet.Infrastructure.Firebase.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Infrastructure.Firebase;

public static class DependencyInjection
{
    public static IServiceCollection AddFirebaseInfra(this IServiceCollection services)
    {
        services.AddSingleton<IPostsRepository, FirestorePostsRepository>();
        services.AddSingleton<IBlobStorage, FirebaseBlobStorage>();

        // Login fake (luego lo cambiamos a Firebase real)
        services.AddSingleton<IAuthService, FakeAuthService>();

        return services;
    }
}

