using System.Globalization;
using Avalonia.Data.Converters;
using CliWrapper.Models;

namespace SdkManager.Converters;

public class PreviewVersionToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is InstalledSdk installedSdk)
        {
            return installedSdk.PreviewVersion is not null;
        }
        
        throw new InvalidOperationException("Value is not of type InstalledSdk");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException("ConvertBack is not implemented");
}