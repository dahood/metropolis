using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.Extensions;
using Metropolis.Api.IO;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.CSharp
{
    [TestFixture]
    public class CollectAssembliesTest : StrictMockBaseTest
    {
        private CollectAssemblies collectAssemblies;

        private Mock<IFileSystem> fileSystem;
        private string[] dllFiles;
        private string[] exeFiles;
        private MetricsCommandArguments args;

        [SetUp]
        public void SetUp()
        {
            fileSystem = CreateMock<IFileSystem>();

            args = new MetricsCommandArguments
            {
                IgnoreFile = Path.Combine(Environment.CurrentDirectory, ".metropolisignore"),
                SourceDirectory = @"c:\sourceDir",
                BuildOutputFolder = @"c:\buildoutput"
            };
            
            dllFiles = new[] { "Assembly1.dll", "Assembly2.dll", "Assembly3.dll", "Assembly4.dll", "Assembly5.dll", "Assembly6.dll" };
            exeFiles = new[] { "program.exe" };
            fileSystem.Setup(x => x.GetFiles(args.BuildOutputFolder, "*.dll")).Returns(dllFiles);
            fileSystem.Setup(x => x.GetFiles(args.BuildOutputFolder, "*.exe")).Returns(exeFiles);

            collectAssemblies = new CollectAssemblies(fileSystem.Object);
        }
        

        [Test]
        public void Should_Gather_All_Assemblies_Excluding_Those_Defined_In_The_Ignore_File()
        {
            fileSystem.Setup(x => x.ReadIgnoreFile(args.IgnoreFile))
                      .Returns(new[] {"Assembly1.dll","Assembly2.dll","Assembly4.cll"});

            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain("Assembly3.dll", "Assembly5.dll", "Assembly6.dll", "program.exe");
        }

        [Test]
        public void Should_Gather_All_Assemblies_Ignore_File_Missing()
        {
            fileSystem.Setup(x => x.ReadIgnoreFile(args.IgnoreFile)).Returns(Enumerable.Empty<string>());

            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain(dllFiles.Append(exeFiles));
        }
    }
}
