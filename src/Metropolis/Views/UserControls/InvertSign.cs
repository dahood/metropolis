using System;
using System.Windows.Data;

namespace Metropolis.Views.UserControls
{
    [ValueConversion(typeof(int), typeof(int))]
    public class InvertSignConverter : IValueConverter
    {
        public static InvertSignConverter Instance = new InvertSignConverter();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (double)value;
            return (val * -1);            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (double)value;
            return (val * -1);            
        }
    }
}
