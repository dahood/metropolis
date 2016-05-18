using FluentAssertions;
using Metropolis.Api.Parsers;
using Metropolis.Api.Parsers.CsvParsers;
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
                .And.BeAssignableTo<VisualStudioMetricsParser>();
        }

        [Test]
        public void Toxicity()
        {
            factory.ParserFor(ParseType.RichardToxicity)
                .Should().NotBeNull()
                .And.BeAssignableTo<ToxicityParser>();
        }

        [Test]
        public void PuppCrawler()
        {
            factory.ParserFor(ParseType.PuppyCrawler)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesParser>();
        }

        [Test]
        public void Eslink()
        {
            factory.ParserFor(ParseType.EsLint)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesParser>();
        }

        [Test]
        public void SlocECMA()
        {
            factory.ParserFor(ParseType.SlocEcma)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<SourceLinesOfCodeParser>();
        }

        [Test]
        public void SlocCSharp()
        {
            factory.ParserFor(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeParser>();
        }

        [Test]
        public void SlocJava()
        {
            factory.ParserFor(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeParser>();
        }
    }
}