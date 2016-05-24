using System;
using System.Globalization;
using System.Windows.Data;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.ValueConverters
{
    public class RepositorySourceTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RepositorySourceType)
                return value.ToString();
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var repoType = value as string;
            if (repoType == null | repoType.IsEmpty())
                return null;

            return repoType?.ToEnumExact<RepositorySourceType>();
        }
    }
}