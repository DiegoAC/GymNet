using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Posts.CreatePost.ICreatePostHandler, Posts.CreatePost.CreatePostHandler>();
        return services;
    }
}
