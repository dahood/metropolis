using System;
using System.IO;
using FluentAssertions;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.IO;
using Metropolis.Api.Utilities;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Metropolis.Test.TestHelpers;
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
        private MetricsCommandArguments args;
        private const string SourceDirectory = @"c:\sourceDir";
        private const string TestIgnoreFile = "TestIgnoreFiles.txt";

        [SetUp]
        public void SetUp()
        {
            TestIgnoreFile.RemoveFileIfExists();

            fileSystem = CreateMock<IFileSystem>();
            collectAssemblies = new CollectAssemblies(fileSystem.Object);

            dllFiles = new[] { "Assembly1.dll", "Assembly2.dll", "Assembly3.dll", "Assembly4.dll", "Assembly5.dll", "Assembly6.dll" };
            fileSystem.Setup(x => x.GetFiles(SourceDirectory, "*.dll")).Returns(dllFiles);

            args = new MetricsCommandArguments
            {
                IgnoreFile = Path.Combine(Environment.CurrentDirectory, TestIgnoreFile),
                SourceDirectory = SourceDirectory
            };
        }

        [TearDown]
        public void TearDown()
        {
            TestIgnoreFile.RemoveFileIfExists();
        }

        [Test]
        public void Should_Gather_All_Assemblies_Excluding_Those_Defined_In_The_Ignore_File()
        {
            CreateIgnoreFileWith("Assembly1.dll", "Assembly3.dll", "Assembly4.dll");
            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain("Assembly2.dll", "Assembly5.dll", "Assembly6.dll");
        }

        [Test]
        public void Should_Gather_All_Assemblies_Ignore_File_Missing()
        {
            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain(dllFiles);
        }

        [Test]
        public void Should_Gather_All_Assemblies_No_Ignore_File_Provided()
        {
            args.IgnoreFile = null;
            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain(dllFiles);
        }

        [Test]
        public void Should_Gather_All_Assemblies_Ignore_File_Empty()
        {
            CreateIgnoreFileWith();
            var assemblies = collectAssemblies.GatherAssemblies(args);

            assemblies.Should().NotBeNullOrEmpty();
            assemblies.Should().Contain(dllFiles);
        }

        private static void CreateIgnoreFileWith(params string[] toIgnore)
        {
            toIgnore.Should().NotBeNull();
            File.WriteAllLines(TestIgnoreFile, toIgnore);
        }
    }
}
