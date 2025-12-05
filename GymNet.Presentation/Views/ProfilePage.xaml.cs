using GymNet.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Presentation.Views;

public partial class ProfilePage : ContentPage
{
    public ProfilePage()
    {
        InitializeComponent();

        var vm = MauiProgram.Services.GetRequiredService<ProfileViewModel>();
        BindingContext = vm;
    }
}




