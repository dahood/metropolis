using CsvHelper.TypeConversion;
using FluentAssertions;
using Metropolis.Api.Readers.CsvReaders.TypeConverters.Checkstyles;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CheckStyles
{
    [TestFixture]
    public class CheckstylesConverterTests
    {
        [Test]
        public void ClassConverter()
        {
            var converter = new CheckstylesClassConverter();

            converter.CanConvertFrom(typeof (string)).Should().BeTrue();
            converter.CanConvertFrom(typeof (int)).Should().BeFalse();
            converter.CanConvertTo(typeof (string)).Should().BeTrue();
            converter.CanConvertTo(typeof (int)).Should().BeFalse();

            converter.ConvertFromString(new TypeConverterOptions(), "com.microsoft.MyClass").Should().Be("MyClass");
            converter.ConvertToString(new TypeConverterOptions(), "MyClass").Should().Be("MyClass");
        }

        [Test]
        public void CheckstylesNamespaceConverter()
        {
            var converter = new CheckstylesNamespaceConverter();

            converter.CanConvertFrom(typeof(string)).Should().BeTrue();
            converter.CanConvertFrom(typeof(int)).Should().BeFalse();
            converter.CanConvertTo(typeof(string)).Should().BeTrue();
            converter.CanConvertTo(typeof(int)).Should().BeFalse();
            converter.ConvertFromString(new TypeConverterOptions(),"com.microsoft.MyClass" ).Should().Be("com.microsoft");
            converter.ConvertToString(new TypeConverterOptions(),"com.microsoft" ).Should().Be("com.microsoft");
        }
    }
}
