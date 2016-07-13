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

        private readonly string twoDuplicate =
            "lines,tokens,occurrences" + Environment.NewLine +
            @"47,196,2," +
            @"106,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java," +
            @"104,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java" + Environment.NewLine +
            @"10,33,2," +
            @"551,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java," +
            @"566,C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java";

        private readonly string manyDuplicatesInsideSameFile =
            "lines,tokens,occurrences" + Environment.NewLine +
            @"11,66,2,1027,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java,1069,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java" + Environment.NewLine +
            @"11,66,2,1048,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java,1070,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java" + Environment.NewLine +
            @"10,61,2,1028,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java,1048,C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java";


        [Test]
        public void ShouldParseOneDuplicate()
        {
            var codebase = parser.Parse(new StringReader(oneDuplicate));

            codebase.InstanceCount().Should().Be(2);
            codebase.AllInstances[0].DuplicateLines.Should().Be(47);
            codebase.AllInstances[0].PhysicalPath.Path.Should()
                .Be(@"C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java");
            codebase.AllInstances[0].Duplicates.Count.Should().Be(1);


            codebase.AllInstances[0].Duplicates[0].LineNumber.Should().Be(106);
            codebase.AllInstances[0].Duplicates[0].CopyCats.Length.Should().Be(1);
            codebase.AllInstances[0].Duplicates[0].CopyCats[0].LineNumber.Should().Be(104);
            codebase.AllInstances[0].Duplicates[0].CopyCats[0].Location.Path.Should()
                .Be(@"C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java");

            codebase.AllInstances[1].DuplicateLines.Should().Be(47);
            codebase.AllInstances[1].PhysicalPath.Path.Should()
                .Be(@"C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java");
            codebase.AllInstances[1].Duplicates.Count.Should().Be(1);

            codebase.AllInstances[1].Duplicates[0].CopyCats.Length.Should().Be(1);
            codebase.AllInstances[1].Duplicates[0].CopyCats[0].LineNumber.Should().Be(106);
            codebase.AllInstances[1].Duplicates[0].CopyCats[0].Location.Path.Should()
    .Be(@"C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedBatchThroughputTest.java");
        }

        [Test]
        public void ShouldParseTwoDuplicates()
        {
            var codebase = parser.Parse(new StringReader(twoDuplicate));

            codebase.InstanceCount().Should().Be(2);
            codebase.AllInstances[0].DuplicateLines.Should().Be(57);
            codebase.AllInstances[0].Duplicates.Count.Should().Be(2);
            codebase.AllInstances[0].Duplicates[0].CopyCats.Length.Should().Be(1);
            codebase.AllInstances[0].Duplicates[0].CopyCats[0].Location.Path.Should()
                .Be(@"C:\Dev\disruptor\src\perftest\java\com\lmax\disruptor\sequenced\ThreeToOneSequencedThroughputTest.java");

            codebase.AllInstances[1].DuplicateLines.Should().Be(57);
            codebase.AllInstances[1].Duplicates.Count.Should().Be(2);
            codebase.AllInstances[1].Duplicates[0].CopyCats.Length.Should().Be(1);
        }

        [Test]
        public void ShouldParseFileWithManyInternalDuplicates()
        {
            var codebase = parser.Parse(new StringReader(manyDuplicatesInsideSameFile));

            codebase.InstanceCount().Should().Be(1);
            codebase.AllInstances[0].DuplicateLines.Should().Be(64);
            codebase.AllInstances[0].Duplicates.Count.Should().Be(6);

            codebase.AllInstances[0].Duplicates[0].CopyCats.Length.Should().Be(1);
            codebase.AllInstances[0].Duplicates[0].CopyCats[0].LineNumber.Should().Be(1069);
            codebase.AllInstances[0].Duplicates[0].CopyCats[0].Location.Path.Should()
                .Be(@"C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java");

            codebase.AllInstances[0].Duplicates[1].CopyCats.Length.Should().Be(1);
            codebase.AllInstances[0].Duplicates[1].CopyCats[0].LineNumber.Should().Be(1027);
            codebase.AllInstances[0].Duplicates[1].CopyCats[0].Location.Path.Should()
                .Be(@"C:\dev\disruptor\src\main\java\com\lmax\disruptor\RingBuffer.java");
        }
    }
}