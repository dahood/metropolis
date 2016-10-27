using System;
using System.IO;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Readers.VersionControlReaders;
using Metropolis.Test.Fixtures;
using NUnit.Framework;

namespace Metropolis.Test.Api.Readers.VersionControlReaders
{
    [TestFixture]
    public class GitLogReaderTest
    {
        private readonly string oneCommit =
            "--12ad430--2016-09-30--Jonathan McCracken" + Environment.NewLine +
            "2	1	Metropolis.Api/Domain/CodeBase.cs" + Environment.NewLine +
            "8	0	Metropolis/Views/Canvas.xaml.cs" + Environment.NewLine +
            Environment.NewLine;

        private readonly CodeBase twoFileCodebase = CodeGraphFixture.Metropolis;

        [Test]
        public void Should_Parse_Commit_Entry()
        {
            var reader = new GitLogReader();

            var result = reader.Parse(new StringReader(oneCommit), twoFileCodebase);

            result.Commits.Count.Should().Be(1);
            result.Commits[0].CommitHash.Should().Be("12ad430");
            result.Commits[0].CommitTime.Should().Be(new DateTime(2016,9,30));
            result.Commits[0].AuthorName.Should().Be("Jonathan McCracken");
//
//            result.AllInstances.Count.Should().Be(2);
//            result.AllInstances[0].RevisionCount.Should().Be(1);
//            result.AllInstances[0].RevisionMagnitude.Should().Be(3);
        }
    }
}