using GymNet.Application;
using GymNet.Infrastructure.Firebase;
using GymNet.Infrastructure.Local;
using GymNet.Presentation.Services;
using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation;

public static class MauiProgram
{
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

        builder.Services
            .AddApplication()
            .AddFirebaseInfra()
            .AddLocalInfra();

        // Store compartido
        builder.Services.AddSingleton<FakeFeedStore>();

        // ViewModels
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<FeedViewModel>();
        builder.Services.AddSingleton<PostComposerViewModel>();
        builder.Services.AddSingleton<ProfileViewModel>();
        builder.Services.AddSingleton<TodayWorkoutViewModel>();
        builder.Services.AddTransient<PostDetailViewModel>();

        return builder.Build();
    }
}






