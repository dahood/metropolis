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
                @"C:\blah\init.js,1733,1102,440,178,262,10,205";

            var codeBase = ParseUsingData(new[] {Heading, line});

            var expected = new Instance(new CodeBag(@"C:\blah", CodeBagType.Directory, @"C:\blah"),  "init.js", new Location(@"C:\blah\init.js"))
            {LinesOfCode = 1102};

            AssertHasOneClassEqualTo(expected, codeBase);
        }
    }
}