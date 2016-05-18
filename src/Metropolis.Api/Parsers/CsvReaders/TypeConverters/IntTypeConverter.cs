using CsvHelper.TypeConversion;

namespace Metropolis.Api.Parsers.CsvReaders.TypeConverters
{
    public class IntTypeConverter : BaseTypeConverter<int>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            if (text == @"n/a") return 0;
            return string.IsNullOrEmpty(text) ? 0 : int.Parse(text.Replace(",",""));
        }
    }
}