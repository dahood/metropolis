using FluentAssertions;
using Metropolis.Api.Core.Parsers.CsvParsers.TypeConverters.Sloc;
using NUnit.Framework;

namespace Metropolis.Test.Parsers.CsvParsers.TypeConverters
{
    [TestFixture]
    public class CsvTypeConverterTests
    {
        [Test]
        public void SourceLinesOfCodeNamespaceConverter()
        {
            var result= new SourceLinesOfCodeNamespaceConverter().ConvertFromString(null, CsvParseTestHelper.SourceLinesOfCodeLine);
            result.Should().Be(@"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw");
        }
        [Test]
        public void SourceLinesOfCodeClassConverter()
        {
            var result= new SourceLinesOfCodeClassConverter().ConvertFromString(null, CsvParseTestHelper.SourceLinesOfCodeLine);
            result.Should().Be("0.init.js");
        }
    }

    public static class CsvParseTestHelper
    {
        public static string SourceLinesOfCodeLine => @"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw\0.init.js";
    }
}
