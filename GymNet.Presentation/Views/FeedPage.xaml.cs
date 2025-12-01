using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation.Views;

public partial class FeedPage : ContentPage
{
    private readonly FeedViewModel _vm;

    public FeedPage(FeedViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadAsyncCommand.ExecuteAsync(null);
    }

    private async void OnComposeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//composer");
    }

    private async void OnReloadClicked(object sender, EventArgs e)
    {
        await _vm.LoadAsyncCommand.ExecuteAsync(null);
    }
}



