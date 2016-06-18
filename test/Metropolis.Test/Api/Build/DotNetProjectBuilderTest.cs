using FluentAssertions;
using Metropolis.Api.Build;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.IO;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Build
{
    [TestFixture]
    public class DotNetProjectBuilderTest : StrictMockBaseTest
    {
        DotNetProjectBuilder builder;
        private Mock<IBuildEnvironment> buildEnvironment;
        private Mock<IRunPowerShell> runPowerShell;
        private Mock<IFileSystem> fileSystem;
        private ProjectBuildArguments args;

        [SetUp]
        public void SetUp()
        {buildEnvironment = CreateMock<IBuildEnvironment>();
            runPowerShell = CreateMock<IRunPowerShell>();
            fileSystem = CreateMock<IFileSystem>();
            args = new ProjectBuildArguments { BuildOutputFolder = @"c:\buildfolder", ProjectName = @"c:\project.sln", SourceType = RepositorySourceType.CSharp };

            builder = new DotNetProjectBuilder(buildEnvironment.Object, runPowerShell.Object, fileSystem.Object);
        }

        [Test]
        public void Build()
        {
            var buildArtifacts = new[] { new FileDto { Name = "app.exe" } };

            fileSystem.Setup(x => x.CleanFolder(args.BuildOutputFolder));
            buildEnvironment.Setup(x => x.MsBuildPath).Returns(@"c:\build.exe");
            runPowerShell.Setup(x => x.Invoke(DotNetProjectBuilder.MsBuildCommand.FormatWith(@"c:\build.exe", args.ProjetFile, args.BuildOutputFolder)));
            fileSystem.Setup(x => x.FindAllBinaries(args.BuildOutputFolder)).Returns(buildArtifacts);

            var result = builder.Build(args);

            result.Should().NotBeNull();
            result.Artifacts.Should().Contain(buildArtifacts);
        }

    }
}