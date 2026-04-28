using System.Globalization;
using Microsoft.Maui.Controls;

namespace login.Converters
{
    public class HtmlSourceConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType,
                               object? parameter, CultureInfo culture)
            => new HtmlWebViewSource { Html = value?.ToString() ?? "" };

        public object? ConvertBack(object? value, Type targetType,
                                   object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
