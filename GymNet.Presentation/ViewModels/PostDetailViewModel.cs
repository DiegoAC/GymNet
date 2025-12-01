using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Presentation.Models;
using GymNet.Presentation.Services;
using Microsoft.Maui.Controls;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

[QueryProperty(nameof(PostId), "postId")]
public partial class PostDetailViewModel : ObservableObject
{
    private readonly FakeFeedStore _store;
    private static readonly ObservableCollection<PostComment> EmptyComments = new();

    [ObservableProperty] private FeedPost? post;
    [ObservableProperty] private string newCommentText = "";
    [ObservableProperty] private bool isBusy;

    private string? postId;
    public string? PostId
    {
        get => postId;
        set
        {
            postId = value;
            if (!string.IsNullOrWhiteSpace(postId))
            {
                _ = LoadAsync(postId);
            }
        }
    }

    public ObservableCollection<PostComment> Comments =>
        Post?.Comments ?? EmptyComments;

    public IAsyncRelayCommand AddCommentCommand { get; }

    public PostDetailViewModel(FakeFeedStore store)
    {
        _store = store;
        AddCommentCommand = new AsyncRelayCommand(AddCommentAsync, () => CanAddComment);
    }

    public bool CanAddComment =>
        !IsBusy &&
        Post is not null &&
        !string.IsNullOrWhiteSpace(NewCommentText);

    partial void OnNewCommentTextChanged(string value)
        => AddCommentCommand.NotifyCanExecuteChanged();

    partial void OnIsBusyChanged(bool value)
        => AddCommentCommand.NotifyCanExecuteChanged();

    private Task LoadAsync(string id)
    {
        var found = _store.FindById(id);
        Post = found;
        OnPropertyChanged(nameof(Comments));
        return Task.CompletedTask;
    }

    private async Task AddCommentAsync()
    {
        if (!CanAddComment || Post is null) return;

        try
        {
            IsBusy = true;

            var text = NewCommentText.Trim();
            if (string.IsNullOrWhiteSpace(text))
                return;

            _store.AddComment(Post.Id, "Tú", text);
            NewCommentText = "";

            // Notificar a la vista que la colección ha cambiado
            OnPropertyChanged(nameof(Comments));

            await Task.Delay(50); // pequeño delay “realista”
        }
        finally
        {
            IsBusy = false;
        }
    }
}

