using GymNet.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Presentation.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();

        var vm = MauiProgram.Services.GetRequiredService<LoginViewModel>();
        BindingContext = vm;
    }
}






