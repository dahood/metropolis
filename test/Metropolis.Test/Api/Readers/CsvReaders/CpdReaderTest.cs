using FluentAssertions.Common;
using Metropolis.Api.Readers.CsvReaders;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.CsvReaders
{
    [TestFixture]
    public class CpdReaderTest
    {
        [SetUp]
        public void Setup()
        {
            parser = new CpdReader();
        }

        private CpdReader parser;

        [Test, Ignore("wip")]
        public void ShouldParseOneDuplicate()
        {
            var codebase = parser.Parse(@"filename");

            codebase.NumberOfTypes.IsSameOrEqualTo(2);
        }
    }
}