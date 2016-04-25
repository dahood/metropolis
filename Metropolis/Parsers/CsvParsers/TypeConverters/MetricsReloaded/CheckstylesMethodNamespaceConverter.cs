using System.Linq;
using CsvHelper.TypeConversion;

namespace Metropolis.Parsers.CsvParsers.TypeConverters.MetricsReloaded
{
    public class CheckstylesMethodNamespaceConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            var parts = text.Split('.').ToList();
            parts.RemoveRange(parts.Count - 2, 2);
            return string.Join(".", parts);
        }
    }
}