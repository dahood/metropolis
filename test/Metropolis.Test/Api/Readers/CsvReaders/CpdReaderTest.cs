using System;
using System.IO;
using FluentAssertions;
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

        private readonly string oneDuplicate = 
            "lines,tokens,occurrences" + Environment.NewLine +
            @"47,196,2," +
            @"106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java," +
            @"104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java";

        [Test]
        public void ShouldParseOneDuplicate()
        {
            var codebase = parser.Parse(new StringReader(oneDuplicate));

            codebase.InstanceCount().Should().Be(2);
            codebase.AllInstances[0].DuplicateLines.Should().Be(47);
            codebase.AllInstances[1].DuplicateLines.Should().Be(47);
        }
    }
}