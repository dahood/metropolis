using System.Linq;
using CsvHelper.TypeConversion;

namespace Metropolis.Api.Readers.CsvReaders.TypeConverters.Sloc
{
    public class SlocDirectoryConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            var parts = text.Split('\\').ToList();
            parts.RemoveRange(parts.Count - 1, 1);
            return string.Join("\\", parts);
        }
    }
}