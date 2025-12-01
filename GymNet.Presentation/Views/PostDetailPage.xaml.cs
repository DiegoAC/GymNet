using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation.Views;

public partial class PostDetailPage : ContentPage
{
    public PostDetailPage(PostDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}


