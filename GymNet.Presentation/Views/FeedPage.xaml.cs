using GymNet.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Presentation.Views;

public partial class FeedPage : ContentPage
{
    public FeedPage()
    {
        InitializeComponent();

        var vm = MauiProgram.Services.GetRequiredService<FeedViewModel>();
        BindingContext = vm;
    }
}




