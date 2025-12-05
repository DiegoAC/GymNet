using GymNet.Application;
using GymNet.Infrastructure.Firebase;
using GymNet.Infrastructure.Local;
using GymNet.Presentation.ViewModels;
using GymNet.Presentation.Services;

namespace GymNet.Presentation;

public static class MauiProgram
{
    public static IServiceProvider Services { get; private set; } = default!;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(f =>
            {
                f.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                f.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Capas de aplicación e infraestructura
        builder.Services
            .AddApplication()
            .AddFirebaseInfra(options =>
            {
                options.ApiKey = "AIzaSyBJc4KKyr6SW8InNal7fVduQzIIH1DPOQQ";
                options.ProjectId = "gymnet-social";
            })
            .AddLocalInfra();

        // Servicios de presentación / stores
        builder.Services.AddSingleton<FakeFeedStore>();

        // ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<FeedViewModel>();
        builder.Services.AddSingleton<PostComposerViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<TodayWorkoutViewModel>();
        builder.Services.AddTransient<PostDetailViewModel>();

        var app = builder.Build();
        Services = app.Services;

        return app;
    }
}











