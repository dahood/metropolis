using System.Linq;
using CsvHelper.TypeConversion;

namespace Metropolis.Api.Readers.CsvReaders.TypeConverters.Sloc
{
    public class SlocFileNameConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            var items = text.Split('\\').Last().Split('.').ToList();
            return string.Join(".", items);
        }
    }
}