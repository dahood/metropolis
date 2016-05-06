using System.Linq;
using CsvHelper.TypeConversion;

namespace Metropolis.Parsers.CsvParsers.TypeConverters.Sloc
{
    public class SourceLinesOfCodeClassConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            var items = text.Split('\\').Last().Split('.').ToList();
            return string.Join(".", items);
        }
    }
}