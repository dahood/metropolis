using Metropolis.Api.Domain;
using Metropolis.Api.Parsers.CsvParsers;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers.CsvReaders
{
    [TestFixture]
    public class SourceLinesOfCodeClassParserTest : CsvParsersBaseTest<SourceLinesOfCodeParser>
    {
        private const string Heading = "Path,Physical,Source,Comment,Single-line comment,Block comment,Mixed,Empty";

        [Test]
        public void Can_Parse()
        {
            const string line =
                @"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw\0.init.js,1733,1102,440,178,262,10,205";

            var codeBase = ParseUsingData(new[] {Heading, line});

            var expected = new Instance(@"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw",
                "0.init.js")
            {LinesOfCode = 1102};

            AssertHasOneClassEqualTo(expected, codeBase);
        }
    }
}