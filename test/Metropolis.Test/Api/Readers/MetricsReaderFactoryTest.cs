using FluentAssertions;
using Metropolis.Api.Readers;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers
{
    [TestFixture]
    public class MetricsReaderFactoryTest
    {
        readonly MetricsReaderFactory factory = new MetricsReaderFactory();

        [Test]
        public void VidualStudio()
        {
            factory.GetReader(ParseType.VisualStudio)
                .Should().NotBeNull()
                .And.BeAssignableTo<VisualStudioMetricsReader>();
        }

        [Test]
        public void Toxicity()
        {
            factory.GetReader(ParseType.RichardToxicity)
                .Should().NotBeNull()
                .And.BeAssignableTo<ToxicityReader>();
        }

        [Test]
        public void PuppCrawler()
        {
            factory.GetReader(ParseType.PuppyCrawler)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void Eslink()
        {
            factory.GetReader(ParseType.EsLint)
                .Should().NotBeNull()
                .And.BeAssignableTo<CheckStylesReader>();
        }

        [Test]
        public void SlocECMA()
        {
            factory.GetReader(ParseType.SlocEcma)
                   .Should().NotBeNull()
                   .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }

        [Test]
        public void SlocCSharp()
        {
            factory.GetReader(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }

        [Test]
        public void SlocJava()
        {
            factory.GetReader(ParseType.SlocCSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<SourceLinesOfCodeReader>();
        }
    }
}