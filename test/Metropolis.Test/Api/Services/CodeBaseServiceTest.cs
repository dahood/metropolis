using FluentAssertions;
using Metropolis.Api.Build;
﻿using System.IO;
using Metropolis.Api.Domain;
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

            codebaseService = new CodebaseService(parserFactory.Object, repository.Object, projectBuilderFactory.Object);
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
    }
}
