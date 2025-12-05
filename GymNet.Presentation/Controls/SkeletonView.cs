using Microsoft.Maui.Controls.Shapes;

namespace GymNet.Presentation.Controls;

/// <summary>
/// Skeleton loader control que muestra un placeholder animado mientras se cargan datos
/// </summary>
public class SkeletonView : ContentView
{
    private readonly BoxView _shimmer;
    private bool _isAnimating;

    public SkeletonView()
    {
        // Crear el contenedor principal
        var container = new Grid();

        // Fondo base gris
        var background = new BoxView
        {
            BackgroundColor = Color.FromArgb("#1E293B"),
            CornerRadius = 12
        };

        // Efecto shimmer que se mueve
        _shimmer = new BoxView
        {
            BackgroundColor = Color.FromArgb("#334155"),
            Opacity = 0.5,
            CornerRadius = 12
        };

        container.Children.Add(background);
        container.Children.Add(_shimmer);

        Content = container;
    }

    protected override void OnParentSet()
    {
        base.OnParentSet();

        if (Parent != null)
        {
            StartAnimation();
        }
        else
        {
            StopAnimation();
        }
    }

    private async void StartAnimation()
    {
        if (_isAnimating) return;
        _isAnimating = true;

        while (_isAnimating && _shimmer != null)
        {
            await _shimmer.FadeTo(0.3, 800, Easing.SinInOut);
            await _shimmer.FadeTo(0.7, 800, Easing.SinInOut);
        }
    }

    private void StopAnimation()
    {
        _isAnimating = false;
    }
}
