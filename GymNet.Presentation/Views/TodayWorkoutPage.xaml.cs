using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation.Views;

public partial class TodayWorkoutPage : ContentPage
{
    public TodayWorkoutPage(TodayWorkoutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}

