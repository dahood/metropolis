using System;
using System.IO;
using FluentAssertions;
using Metropolis.Api.Readers.XmlReaders.FxCop;
using Metropolis.Test.Fixtures;
using Metropolis.Test.TestHelpers;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.XmlReaders.FxCop
{
    [TestFixture]
    public class FxCopMetricsReaderTest
    {
        private const string FileName = "FxCop_Sample_Metrics.xml";
        private string pathToMetricsFile;

        FxCopMetricsReader reader;

        [SetUp]
        public void SetUp()
        {
            pathToMetricsFile = Path.Combine(Environment.CurrentDirectory, FileName);
            pathToMetricsFile.RemoveFileIfExists();
            File.WriteAllText(pathToMetricsFile, MetricsDataFixture.FxCopMetricsMetricsData);

            reader = new FxCopMetricsReader();
        }

        [TearDown]
        public void TearDown()
        {
            pathToMetricsFile.RemoveFileIfExists();
        }

        [Test]
        public void Can_Read_Metropolis_Metrics_File()
        {
            var codeBase = reader.Parse(pathToMetricsFile);
            codeBase.Should().NotBeNull();


        }
    }
}
