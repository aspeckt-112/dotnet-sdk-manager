using System.Globalization;
using Avalonia.Data.Converters;

namespace SdkManager.Converters;

public class VersionToStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Version version)
        {
            return version.ToString();
        }

        return "N/A";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException("ConvertBack is not implemented");
}