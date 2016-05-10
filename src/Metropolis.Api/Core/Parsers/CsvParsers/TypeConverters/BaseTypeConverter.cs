using System;
using CsvHelper.TypeConversion;

namespace Metropolis.Api.Core.Parsers.CsvParsers.TypeConverters
{
    public abstract class BaseTypeConverter<T> : ITypeConverter
    {
        public virtual string ConvertToString(TypeConverterOptions options, object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        public abstract object ConvertFromString(TypeConverterOptions options, string text);

        public bool CanConvertFrom(Type type)
        {
            return typeof(T) == type || type == typeof(string);
        }

        public bool CanConvertTo(Type type)
        {
            return typeof(T) == type || type == typeof(string);
        }
    }
}