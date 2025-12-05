using GymNet.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GymNet.Presentation.Views;

[QueryProperty(nameof(PostId), "postId")]
public partial class PostDetailPage : ContentPage
{
    public string? PostId { get; set; }

    public PostDetailPage()
    {
        InitializeComponent();

        var vm = MauiProgram.Services.GetRequiredService<PostDetailViewModel>();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PostDetailViewModel vm && !string.IsNullOrEmpty(PostId))
        {
            vm.LoadPost(PostId);
        }
    }
}



