using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace PrviProjektniZadatakHCI.Util
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (bool)value ? Visibility.Collapsed : Visibility.Visible;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (Visibility)value != Visibility.Visible;
    }
}
