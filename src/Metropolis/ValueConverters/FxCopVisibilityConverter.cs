using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metropolis.ValueConverters
{
    public class FxCopVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fxCopInstalled = value as bool?;

            return fxCopInstalled != null && fxCopInstalled.Value? Visibility.Hidden: Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}