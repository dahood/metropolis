using Metropolis.Domain;
using Metropolis.Parsers.CsvParsers;
using NUnit.Framework;

namespace Test.Metropolis.Parsers.CsvParsers
{
    [TestFixture]
    public class SourceLinesOfCodeClassParserTest : CsvParsersBaseTest<SourceLinesOfCodeParser>
    {
        private const string Heading = "Path,Physical,Source,Comment,Single-line comment,Block comment,Mixed,Empty";

        [Test]
        public void Can_Parse()
        {
            const string line = @"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw\0.init.js,1733,1102,440,178,262,10,205";

            var codeBase = ParseUsingData(new[] { Heading, line });

            var expected = new Class(@"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw", "0.init.js") {LinesOfCode = 1733};

            AssertHasOneClassEqualTo(expected, codeBase);
        }
    }
}
