using System.Globalization;

namespace GymNet.Presentation.Converters;

public sealed class NullToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null)
            return false;

        if (value is string s)
            return !string.IsNullOrWhiteSpace(s);

        // Para cualquier otro tipo, si no es null -> true
        return true;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}



