using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Presentation.Models;
using GymNet.Presentation.Services;
using Microsoft.Maui.Controls;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

public partial class FeedViewModel : ObservableObject
{
    private readonly FakeFeedStore _store;

    [ObservableProperty] private bool isBusy;

    public ObservableCollection<FeedPost> Posts => _store.Posts;

    public IAsyncRelayCommand LoadAsyncCommand { get; }
    public IRelayCommand<FeedPost> ToggleLikeCommand { get; }
    public IAsyncRelayCommand<FeedPost> OpenPostCommand { get; }

    public FeedViewModel(FakeFeedStore store)
    {
        _store = store;
        LoadAsyncCommand = new AsyncRelayCommand(LoadAsync);
        ToggleLikeCommand = new RelayCommand<FeedPost>(ToggleLike);
        OpenPostCommand = new AsyncRelayCommand<FeedPost>(OpenPostAsync);
    }

    private async Task LoadAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            await Task.Delay(200);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ToggleLike(FeedPost? post)
    {
        if (post is null) return;
        _store.ToggleLike(post);
    }

    private async Task OpenPostAsync(FeedPost? post)
    {
        if (post is null) return;

        await Shell.Current.GoToAsync($"postDetail?postId={post.Id}");
    }
}
