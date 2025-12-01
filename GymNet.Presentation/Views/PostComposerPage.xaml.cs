using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation.Views;

public partial class PostComposerPage : ContentPage
{
    public PostComposerPage(PostComposerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}


