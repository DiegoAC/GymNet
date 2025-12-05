using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Application.Abstractions.Identity;

#pragma warning disable MVVMTK0045

namespace GymNet.Presentation.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    [ObservableProperty] private string email = "";
    [ObservableProperty] private string password = "";
    [ObservableProperty] private string? errorMessage;
    [ObservableProperty] private bool isBusy;
    [ObservableProperty] private bool isRegisterMode; // false = login, true = registro

    public IAsyncRelayCommand PrimaryCommand { get; }
    public IRelayCommand ToggleModeCommand { get; }

    public string HeaderText => IsRegisterMode ? "Crear cuenta" : "Iniciar sesión";
    public string PrimaryButtonText => IsRegisterMode ? "Crear cuenta" : "Entrar";
    public string SecondaryText =>
        IsRegisterMode
            ? "¿Ya tienes cuenta? Inicia sesión"
            : "¿Nuevo en GymNet? Crea una cuenta";

    public LoginViewModel(IAuthService authService)
    {
        _authService = authService;

        IsRegisterMode = false;

        // IMPORTANTE: sin CanExecute, siempre se ejecuta al pulsar
        PrimaryCommand = new AsyncRelayCommand(PrimaryAsync);
        ToggleModeCommand = new RelayCommand(ToggleMode);
    }

    partial void OnEmailChanged(string value)
    {
        ErrorMessage = null;
    }

    partial void OnPasswordChanged(string value)
    {
        ErrorMessage = null;
    }

    partial void OnIsRegisterModeChanged(bool value)
    {
        ErrorMessage = null;
        OnPropertyChanged(nameof(HeaderText));
        OnPropertyChanged(nameof(PrimaryButtonText));
        OnPropertyChanged(nameof(SecondaryText));
    }

    private void ToggleMode()
    {
        IsRegisterMode = !IsRegisterMode;
    }

    private async Task PrimaryAsync()
    {
        if (IsBusy)
            return;

        ErrorMessage = null;

        var email = Email?.Trim() ?? "";
        var password = Password?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ErrorMessage = "Introduce correo y contraseña.";
            return;
        }

        try
        {
            IsBusy = true;

            var result = IsRegisterMode
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
            "API_KEY_NOT_VALID" => "API key no válida. Revisa la configuración de Firebase.",
            _ => "Error de autenticación: " + error
        };
    }
}
