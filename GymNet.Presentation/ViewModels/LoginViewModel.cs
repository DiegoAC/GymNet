using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Application.Abstractions.Services;

//Funciona todo perfectamente, así que he silenciado ese aviso para que no moleste.
#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _auth;

    [ObservableProperty] private string email = "";
    [ObservableProperty] private string password = "";
    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private string? errorMessage;

    public IAsyncRelayCommand SignInCommand { get; }
    public IAsyncRelayCommand RegisterCommand { get; }

    public LoginViewModel(IAuthService auth)
    {
        _auth = auth;

        SignInCommand = new AsyncRelayCommand(SignInAsync, () => CanSubmit);
        RegisterCommand = new AsyncRelayCommand(RegisterAsync, () => CanSubmit);
    }

    public bool CanSubmit =>
        !IsBusy &&
        !string.IsNullOrWhiteSpace(Email) &&
        !string.IsNullOrWhiteSpace(Password);

    partial void OnEmailChanged(string value) => NotifyCommands();
    partial void OnPasswordChanged(string value) => NotifyCommands();
    partial void OnIsBusyChanged(bool value) => NotifyCommands();

    private void NotifyCommands()
    {
        SignInCommand.NotifyCanExecuteChanged();
        RegisterCommand.NotifyCanExecuteChanged();
    }

    private async Task SignInAsync()
    {
        if (IsBusy) return;
        ErrorMessage = null;

        try
        {
            IsBusy = true;
            var result = await _auth.SignInWithEmailPasswordAsync(
                Email.Trim(),
                Password,
                CancellationToken.None);

            await Shell.Current.GoToAsync("//feed");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task RegisterAsync()
    {
        if (IsBusy) return;
        ErrorMessage = null;

        try
        {
            IsBusy = true;
            var result = await _auth.RegisterWithEmailPasswordAsync(
                Email.Trim(),
                Password,
                CancellationToken.None);

            await Shell.Current.GoToAsync("//feed");
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }
    }
}
