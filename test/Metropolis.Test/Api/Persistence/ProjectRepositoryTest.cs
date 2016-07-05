using System;
using System.IO;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Test.Fixtures;
using NUnit.Framework;

namespace Metropolis.Test.Api.Persistence
{
    [TestFixture]
    public class ProjectRepositoryTest
    {
        private CodeBase codebase;
        private ProjectRepository projectRepository;
        private static string SampleProject => "sample.project";

        [SetUp]
        public void Setup()
        {
            RemoveFile(SampleProject);
            codebase = new CodeBase(CodeGraphFixture.MetropolisGraph);
            projectRepository = new ProjectRepository();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveFile(SampleProject);
        }
        
        [Test]
        public void Should_Load_Default_Project_From_JSON()
        {
            var project = projectRepository.LoadDefault();
            Assert.That(project, Is.Not.Null);
        }

        [Test]
        public void Should_Save_Project_To_JSON()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, SampleProject);
            projectRepository.Save(codebase, filePath);
            //manual verification, eventually should have string comparision
        }

        [Test]
        public void Should_Load_Saved_Project()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, SampleProject);
            projectRepository.Save(codebase, filePath);
            var loaded = projectRepository.Load(filePath);
            loaded.Should().NotBeNull();
        }
        private static void RemoveFile(string testfile)
        {
            if (File.Exists(testfile))
                File.Delete(testfile);
        }
    }
}