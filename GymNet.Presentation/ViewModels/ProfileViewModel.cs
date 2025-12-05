using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GymNet.Application.Abstractions.Identity;

#pragma warning disable MVVMTK0045
namespace GymNet.Presentation.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    public ProfileViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    // En el futuro vendrán de Auth + backend
    [ObservableProperty] private string displayName = "Diego";
    [ObservableProperty] private string email = "diego@example.com";
    [ObservableProperty] private string username = "@diego_lifts";
    [ObservableProperty] private string gymName = "Iron Temple Gym";
    [ObservableProperty] private string level = "Intermedio";
    [ObservableProperty] private int workoutsThisWeek = 4;
    [ObservableProperty] private int streakDays = 7;
    [ObservableProperty] private int followers = 42;
    [ObservableProperty] private int following = 18;

    public string Initials =>
        string.IsNullOrWhiteSpace(DisplayName)
            ? "U"
            : new string(DisplayName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]))
                .Take(2)
                .ToArray());

    [RelayCommand]
    private async Task LogoutAsync()
    {
        // Haptic feedback for better UX
        try
        {
            HapticFeedback.Perform(HapticFeedbackType.Click);
        }
        catch { /* Haptics not supported on all platforms */ }

        await _authService.SignOutAsync();
        await Shell.Current.GoToAsync("//login");
    }

    // En el futuro podrías cargar datos del backend aquí
    public Task LoadAsync()
    {
        // De momento nada, pero dejamos el hook
        return Task.CompletedTask;
    }
}
