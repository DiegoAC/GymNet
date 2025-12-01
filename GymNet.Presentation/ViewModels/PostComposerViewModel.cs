using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Application.Posts.CreatePost;
using Microsoft.Maui.Controls;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

public partial class PostComposerViewModel : ObservableObject
{
    private readonly ICreatePostHandler _handler;
    private const int MaxLength = 280;

    [ObservableProperty] private string text = "";
    [ObservableProperty] private string? lastResult;
    [ObservableProperty] private bool isBusy;

    public int RemainingChars => MaxLength - (Text?.Length ?? 0);

    public bool CanSubmit =>
        !IsBusy &&
        !string.IsNullOrWhiteSpace(Text) &&
        Text.Trim().Length <= MaxLength;

    public IAsyncRelayCommand PublishCommand { get; }

    public PostComposerViewModel(ICreatePostHandler handler)
    {
        _handler = handler;
        PublishCommand = new AsyncRelayCommand(PublishAsync, () => CanSubmit);
    }

    partial void OnTextChanged(string value)
    {
        OnPropertyChanged(nameof(RemainingChars));
        PublishCommand.NotifyCanExecuteChanged();
    }

    partial void OnIsBusyChanged(bool value)
    {
        PublishCommand.NotifyCanExecuteChanged();
    }

    private async Task PublishAsync()
    {
        if (IsBusy) return;

        LastResult = null;

        try
        {
            IsBusy = true;

            var res = await _handler.Handle(
                new CreatePostCommand(Text, null, null),
                CancellationToken.None);

            if (res.IsSuccess)
            {
                LastResult = "Post publicado correctamente.";
                Text = "";

                // Volver al feed
                await Shell.Current.GoToAsync("//feed");
            }
            else
            {
                LastResult = $"Error: {res.Error}";
            }
        }
        catch (Exception ex)
        {
            LastResult = $"Error inesperado: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}



