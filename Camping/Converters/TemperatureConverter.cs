using System;
using System.Globalization;
using System.Windows.Data;

namespace Camping.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class TemperatureConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            return $"{value} C°";
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            return double.Parse(((string?)value ?? "0 C°")[..^4]);
        }
    }
}
