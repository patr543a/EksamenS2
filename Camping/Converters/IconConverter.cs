using Camping.Services;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Camping.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class IconConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            return $"{WeatherService.BaseIconUrl}{value}.png";
        }

        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return null;

            return (value as string)!.Replace(WeatherService.BaseIconUrl, "").Replace(".png", "");
        }
    }
}
