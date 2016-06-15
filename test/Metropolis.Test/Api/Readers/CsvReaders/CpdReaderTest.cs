using System;
using FluentAssertions.Common;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using NUnit.Framework;
using System.IO;
using FluentAssertions;

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
        private readonly string oneDuplicate = "lines,tokens,occurrences" + Environment.NewLine +
            @"47,196,2," +
            @"106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java," +
            @"104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java";

        [Test]
        public void ShouldParseOneDuplicate()
        {
            
            var codebase = parser.Parse(new StringReader(oneDuplicate));

            //codebase.NumberOfTypes.Should().Be(2);
        }
    }
}