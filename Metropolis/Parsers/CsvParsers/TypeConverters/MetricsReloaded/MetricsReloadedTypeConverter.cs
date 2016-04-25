using CsvHelper.TypeConversion;

namespace Metropolis.Parsers.CsvParsers.TypeConverters.MetricsReloaded
{
    public class MetricsReloadedTypeConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            var parts = text.Split('.');
            return parts[parts.Length - 2];
        }
    }
}