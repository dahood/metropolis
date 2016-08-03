using FluentAssertions;
using Metropolis.Api.Build;
﻿using System.IO;
using System.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Api.Persistence;
using Metropolis.Api.Readers;
using Metropolis.Api.Services;
using Metropolis.Common.Models;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    [TestFixture]
    public class CodeBaseServiceTest : StrictMockBaseTest
    {
        private CodebaseService codebaseService;
        private Mock<IMetricsReaderFactory> parserFactory;
        private Mock<IProjectRepository> repository;
        private Mock<IInstanceReader> classParser;
        private Mock<IProjectBuildFactory> projectBuilderFactory;
        private Mock<IFileSystem> fileSystem;

        private const string ProjectName = "myProject";
        private const string FileName = @"C:\\myfolder\mycodebase.project";
        private readonly CodeBase workspace = CodeBase.Empty();
        private readonly StringReader stringReader = new StringReader(FileName);


        [SetUp]
        public void SetUp()
        {
            parserFactory = CreateMock<IMetricsReaderFactory>();
            repository = CreateMock<IProjectRepository>();
            classParser = CreateMock<IInstanceReader>();
            projectBuilderFactory = CreateMock<IProjectBuildFactory>();
            fileSystem = CreateMock<IFileSystem>();

            codebaseService = new CodebaseService(parserFactory.Object, repository.Object, projectBuilderFactory.Object, fileSystem.Object);
        }

        [Test]
        public void Save()
        {
            repository.Setup(x => x.Save(workspace, FileName));
            codebaseService.Save(workspace, FileName);
        }

        [Test]
        public void Load()
        {
            repository.Setup(x => x.Load(FileName)).Returns(workspace);
            codebaseService.Load(FileName).Should().BeSameAs(workspace);
        }

        [Test]
        public void LoadDefault()
        {
            repository.Setup(x => x.LoadDefault()).Returns(workspace);
            codebaseService.LoadDefault().Should().BeSameAs(workspace);
        }

        [Test]
        public void GetToxicity()
        {
            parserFactory.Setup(x => x.GetReader(ParseType.RichardToxicity)).Returns(classParser.Object);
            classParser.Setup(x => x.Parse(stringReader)).Returns(workspace);
            codebaseService.GetToxicity(stringReader).Should().BeSameAs(workspace);
        }

        [Test]
        public void GetVisualStudioMetrics()
        {
            parserFactory.Setup(x => x.GetReader(ParseType.VisualStudio)).Returns(classParser.Object);
            classParser.Setup(x => x.Parse(stringReader)).Returns(workspace);
            codebaseService.GetVisualStudioMetrics(stringReader).Should().BeSameAs(workspace);
        }

        [Test]
        public void Get()
        {
            parserFactory.Setup(x => x.GetReader(ParseType.EsLint)).Returns(classParser.Object);
            classParser.Setup(x => x.Parse(stringReader)).Returns(workspace);
            codebaseService.Get(stringReader, ParseType.EsLint).Should().BeSameAs(workspace);
        }
        
        [Test]
        public void BuildSolution()
        {
            var buildArgs = new ProjectBuildArguments(ProjectName, "blah.sln", RepositorySourceType.CSharp, @"C:\project\build\myProject");
            var builder = CreateMock<IProjectBuilder>();

            projectBuilderFactory.Setup(x => x.BuilderFor(buildArgs.SourceType)).Returns(builder.Object);
            builder.Setup(x => x.Build(buildArgs)).Returns(new ProjectBuildResult());

            var result = codebaseService.BuildSolution(buildArgs);

            result.Should().NotBeNull();
            buildArgs.BuildOutputFolder.Should().Be(Path.Combine(@"C:\project\build", buildArgs.ProjectName));
        }

        [Test]
        public void GetIgnoreFIlesForProject()
        {
            var ignoreFiles = new[] {@"c:\ignore1.dll", @"c:\ignore2.exe"};
            fileSystem.Setup(x => x.ReadIgnoreFile(ProjectName)).Returns(ignoreFiles);

            var results = codebaseService.GetIgnoreFilesForProject(ProjectName);
            results.Should().NotBeNull();
            results.Count().Should().Be(2);

            results.First().Should().Be(new FileDto { Name = ignoreFiles.First(), Ignore = true});
            results.Last().Should().Be(new FileDto {Name = ignoreFiles.Last(), Ignore = true});
        }

        [Test]
        public void GetFileResults()
        {
            const string filePath = @"c:\theFile.cs";
            const string fileContents = "this is the file results";
            fileSystem.Setup(x => x.ReadFile(filePath)).Returns(fileContents);

            var results = codebaseService.GetFileContents(filePath);

            results.Should().NotBeNull();
            results.FileName.Should().Be(Path.GetFileName(filePath));
            results.Data.Should().Be(fileContents);
        }

        [Test]
        public void WriteIgnoreFile()
        {
            var toIgnore = new FileDto {Name = @"c:\ignoreMe.cs"};
            var ignoreFile = ".metropolisignore";
            var projectFolder = @"c:\projFolder";
            var projectBuildFolder = @"C:\project\build";

            fileSystem.Setup(x => x.ProjectBuildFolder).Returns(projectBuildFolder);
            fileSystem.Setup(x => x.IgnoreFile).Returns(ignoreFile);
            fileSystem.Setup(x => x.WriteText(Path.Combine(projectFolder, ignoreFile), new[] { toIgnore.Name }));
            
            codebaseService.WriteIgnoreFile(ProjectName, projectFolder, new [] {toIgnore});
        }
    }
}

