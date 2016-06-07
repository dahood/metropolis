using System;
using CsvHelper.TypeConversion;
using FluentAssertions;
using Metropolis.Api.Readers.CsvReaders.TypeConverters;
using Metropolis.Api.Readers.CsvReaders.TypeConverters.Sloc;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CsvReaders.TypeConverters
{
    [TestFixture]
    public class CsvTypeConverterTests
    {
        [Test]
        public void SourceLinesOfCodeClassConverter()
        {
            var converter = new SlocFileNameConverter();

            converter.CanConvertFrom(typeof (string)).Should().BeTrue();
            converter.CanConvertFrom(typeof (int)).Should().BeFalse();
            converter.CanConvertTo(typeof (string)).Should().BeTrue();
            converter.CanConvertTo(typeof (int)).Should().BeFalse();

            converter.ConvertFromString(null, CsvParseTestHelper.SourceLinesOfCodeLine).Should().Be("0.init.js");
            converter.ConvertToString(null, "0.init.js").Should().Be("0.init.js");
        }

        [Test]
        public void SourceLinesOfCodeNamespaceConverter()
        {
            var converter = new SlocDirectoryConverter();
            
            converter.CanConvertFrom(typeof(string)).Should().BeTrue();
            converter.CanConvertFrom(typeof(int)).Should().BeFalse();
            converter.CanConvertTo(typeof(string)).Should().BeTrue();
            converter.CanConvertTo(typeof(int)).Should().BeFalse();

            converter.ConvertFromString(null, CsvParseTestHelper.SourceLinesOfCodeLine)
                     .Should().Be(@"C:\projects\ecommerce\j2ee-apps\commerce.ear\commerce.war\builder\src\js\commerce");
            converter.ConvertToString(null, @"C:\projects\ecommerce\j2ee-apps\commerce.ear")
                     .Should().Be(@"C:\projects\ecommerce\j2ee-apps\commerce.ear");
        }

        [Test]
        public void CanConvert()
        {
            var converter = new IntTypeConverter();

            converter.CanConvertFrom(typeof(int)).Should().BeTrue();
            converter.CanConvertFrom(typeof(string)).Should().BeTrue();
            converter.CanConvertFrom(typeof(DateTime)).Should().BeFalse();
            converter.CanConvertTo(typeof(int)).Should().BeTrue();
            converter.CanConvertTo(typeof(string)).Should().BeTrue();
            converter.CanConvertTo(typeof(DateTime)).Should().BeFalse();

            converter.ConvertFromString(new TypeConverterOptions(), "12").Should().Be(12);
            converter.ConvertFromString(new TypeConverterOptions(), @"n/a").Should().Be(0);

            converter.ConvertToString(new TypeConverterOptions(), 99).Should().Be("99");
        }
    }

    public static class CsvParseTestHelper
    {
        public static string SourceLinesOfCodeLine
            => @"C:\projects\ecommerce\j2ee-apps\commerce.ear\commerce.war\builder\src\js\commerce\0.init.js";
    }
}