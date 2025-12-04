using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Application.Abstractions.Identity;
using Microsoft.Maui.Controls;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    [ObservableProperty] private string email = "";
    [ObservableProperty] private string password = "";
    [ObservableProperty] private string? errorMessage;
    [ObservableProperty] private bool isBusy;

    public IAsyncRelayCommand SignInCommand { get; }
    public IAsyncRelayCommand RegisterCommand { get; }

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;

        SignInCommand = new AsyncRelayCommand(SignInAsync, CanExecuteAuth);
        RegisterCommand = new AsyncRelayCommand(RegisterAsync, CanExecuteAuth);
    }

    private bool CanExecuteAuth()
        => !IsBusy &&
           !string.IsNullOrWhiteSpace(Email) &&
           !string.IsNullOrWhiteSpace(Password);

    partial void OnEmailChanged(string value)
    {
        ErrorMessage = null;
        SignInCommand.NotifyCanExecuteChanged();
        RegisterCommand.NotifyCanExecuteChanged();
    }

    partial void OnPasswordChanged(string value)
    {
        ErrorMessage = null;
        SignInCommand.NotifyCanExecuteChanged();
        RegisterCommand.NotifyCanExecuteChanged();
    }

    partial void OnIsBusyChanged(bool value)
    {
        SignInCommand.NotifyCanExecuteChanged();
        RegisterCommand.NotifyCanExecuteChanged();
    }

    private async Task SignInAsync()
    {
        await ExecuteAuthAsync(isRegister: false);
    }

    private async Task RegisterAsync()
    {
        await ExecuteAuthAsync(isRegister: true);
    }

    private async Task ExecuteAuthAsync(bool isRegister)
    {
        if (!CanExecuteAuth())
            return;

        ErrorMessage = null;

        try
        {
            IsBusy = true;

            var email = Email.Trim();
            var password = Password.Trim();

            var result = isRegister
                ? await _authService.RegisterAsync(email, password)
                : await _authService.SignInAsync(email, password);

            if (result.IsSuccess)
            {
                await Shell.Current.GoToAsync("//feed");
            }
            else
            {
                ErrorMessage = MapFirebaseError(result.Error);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error inesperado: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private static string MapFirebaseError(string? error)
    {
        if (string.IsNullOrWhiteSpace(error))
            return "No se ha podido completar la operación.";

        return error switch
        {
            "EMAIL_EXISTS" => "Ya existe una cuenta con ese correo.",
            "OPERATION_NOT_ALLOWED" => "Este método de acceso no está habilitado.",
            "TOO_MANY_ATTEMPTS_TRY_LATER" => "Demasiados intentos, inténtalo más tarde.",
            "EMAIL_NOT_FOUND" => "No existe ninguna cuenta con ese correo.",
            "INVALID_PASSWORD" => "Contraseña incorrecta.",
            "USER_DISABLED" => "La cuenta ha sido deshabilitada.",
            _ => "Error de autenticación: " + error
        };
    }
}
