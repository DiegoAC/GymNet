using GymNet.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Presentation.Views;

public partial class TodayWorkoutPage : ContentPage
{
    public TodayWorkoutPage()
    {
        InitializeComponent();

        var vm = MauiProgram.Services.GetRequiredService<TodayWorkoutViewModel>();
        BindingContext = vm;
    }
}

