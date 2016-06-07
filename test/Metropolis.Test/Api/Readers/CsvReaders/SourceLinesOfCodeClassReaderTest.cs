using Metropolis.Api.Domain;
using Metropolis.Api.Readers.CsvReaders;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CsvReaders
{
    [TestFixture]
    public class SourceLinesOfCodeClassReaderTest : CsvParsersBaseTest<SlocReader>
    {
        private const string Heading = "Path,Physical,Source,Comment,Single-line comment,Block comment,Mixed,Empty";

        [Test]
        public void Can_Parse()
        {
            const string line =
                @"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw\0.init.js,1733,1102,440,178,262,10,205";

            var codeBase = ParseUsingData(new[] {Heading, line});

            var expected = new Instance((string) "0.init.js", (string) @"C:\projects\shaw-commerce\j2ee-apps\shaw.ear\shaw.war\builder\src\js\shaw", CodeBagType.Directory, string.Empty)
            {LinesOfCode = 1102};

            AssertHasOneClassEqualTo(expected, codeBase);
        }
    }
}