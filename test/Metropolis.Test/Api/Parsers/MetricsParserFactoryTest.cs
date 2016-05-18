using FluentAssertions;
using Metropolis.Api.Parsers;
using Metropolis.Api.Parsers.CsvReaders;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Parsers
{
    [TestFixture]
    public class MetricsParserFactoryTest
    {
        readonly MetricsParserFactory factory = new MetricsParserFactory();

        [Test]
        public void VidualStudio()
        {
            factory.ParserFor(ParseType.VisualStudio)
                .Should().NotBeNull()
                .And.BeAssignableTo<VisualStudioMetricsReader>();
        }

        [Test]
        public void Toxicity()
        {
            factory.ParserFor(ParseType.RichardToxicity)
                .Should().NotBeNull()
                .And.BeAssignableTo<ToxicityReader>();
        }

        [Test]
        public void PuppCrawler()
        {
            factory.ParserFor(ParseType.PuppyCrawler)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void Eslink()
        {
            factory.ParserFor(ParseType.EsLint)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void SlocECMA()
        {
            factory.ParserFor(ParseType.SlocEcma)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }

        [Test]
        public void SlocCSharp()
        {
            factory.ParserFor(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }

        [Test]
        public void SlocJava()
        {
            factory.ParserFor(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }
    }
}