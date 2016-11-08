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
        [SetUp]
        public void setup()
        {
            reader = new GitLogReader();

            result = reader.Parse(new StringReader(oneCommit), twoFileCodebase);
        }

        private readonly string oneCommit =
            "--12ad430--2016-09-30--Jonathan McCracken" + Environment.NewLine +
            "2	1	Metropolis.Api/Domain/CodeBase.cs" + Environment.NewLine +
            "8	0	Metropolis/Views/Canvas.xaml.cs" + Environment.NewLine +
            "-	-	Metropolis/logo.png" + Environment.NewLine +
            Environment.NewLine;

        private readonly CodeBase twoFileCodebase = CodeGraphFixture.Metropolis;

        private GitLogReader reader;
        private CodeBase result;

        [Test]
        public void Should_Parse_Commit_Entry()
        {
            result.Commits.Count.Should().Be(1);
            result.Commits[0].CommitHash.Should().Be("12ad430");
            result.Commits[0].CommitTime.Should().Be(new DateTime(2016, 9, 30));
            result.Commits[0].AuthorName.Should().Be("Jonathan McCracken");

            result.Commits[0].AdditionsAndDeletions[0].AddedLines.Should().Be(2);
            result.Commits[0].AdditionsAndDeletions[0].DeletedLines.Should().Be(1);
            result.Commits[0].AdditionsAndDeletions[0].IsBinary.Should().Be(false);
            result.Commits[0].AdditionsAndDeletions[0].Path.Path.Should().Be("Metropolis.Api/Domain/CodeBase.cs");

            result.Commits[0].AdditionsAndDeletions[1].AddedLines.Should().Be(8);
            result.Commits[0].AdditionsAndDeletions[1].DeletedLines.Should().Be(0);
            result.Commits[0].AdditionsAndDeletions[1].IsBinary.Should().Be(false);
            result.Commits[0].AdditionsAndDeletions[1].Path.Path.Should().Be("Metropolis/Views/Canvas.xaml.cs");

            result.Commits[0].AdditionsAndDeletions[2].AddedLines.Should().Be(0);
            result.Commits[0].AdditionsAndDeletions[2].DeletedLines.Should().Be(0);
            result.Commits[0].AdditionsAndDeletions[2].IsBinary.Should().Be(true);
            result.Commits[0].AdditionsAndDeletions[2].Path.Path.Should().Be("Metropolis/logo.png");
        }

        [Test]
        public void Should_Apply_Commit_Against_Instances_In_CodeBase()
        {
            result.AllInstances[0].VersionHistory.Count.Should().Be(1);
        }
    }
}