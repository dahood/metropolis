﻿using System.Linq;
using CsvHelper.TypeConversion;

namespace Metropolis.Api.Readers.CsvReaders.TypeConverters.Checkstyles
{
    public class CheckstylesClassConverter : BaseTypeConverter<string>
    {
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            return text.Split('.').Last();
        }
    }
}
