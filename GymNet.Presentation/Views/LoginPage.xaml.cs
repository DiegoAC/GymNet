using GymNet.Presentation.ViewModels;

namespace GymNet.Presentation.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        // Estado inicial para animación
        HeaderArea.Opacity = 0;
        HeaderArea.TranslationY = -20;

        Card.Opacity = 0;
        Card.TranslationY = 30;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Animación en paralelo: header sube y card se eleva con fade
        var headerTask = Task.WhenAll(
            HeaderArea.FadeTo(1, 400, Easing.CubicOut),
            HeaderArea.TranslateTo(0, 0, 400, Easing.CubicOut)
        );

        await Task.Delay(80);

        var cardTask = Task.WhenAll(
            Card.FadeTo(1, 450, Easing.CubicOut),
            Card.TranslateTo(0, 0, 450, Easing.CubicOut)
        );

        await Task.WhenAll(headerTask, cardTask);

        // Foco inicial en email
        EmailEntry.Focus();
    }

    // Enter en email -> foco en password
    private void OnEmailCompleted(object sender, EventArgs e)
        => PasswordEntry.Focus();

    // Enter en password -> ejecutar login si se puede
    private void OnPasswordCompleted(object sender, EventArgs e)
    {
        if (BindingContext is LoginViewModel vm &&
            vm.SignInCommand.CanExecute(null))
        {
            vm.SignInCommand.Execute(null);
        }
    }

    // Botón 🌓: cambio de tema claro/oscuro
    private void OnToggleThemeClicked(object sender, EventArgs e)
    {
        // Usamos explícitamente la clase de MAUI, evitando el conflicto con GymNet.Application
        var app = Microsoft.Maui.Controls.Application.Current;
        if (app is null) return;

        var current = app.UserAppTheme == AppTheme.Unspecified
            ? AppTheme.Dark
            : app.UserAppTheme;

        app.UserAppTheme = current == AppTheme.Dark
            ? AppTheme.Light
            : AppTheme.Dark;
    }

}

