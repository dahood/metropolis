using System;
using System.IO;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Persistence;
using Metropolis.Test.Fixtures;
using NUnit.Framework;

namespace Metropolis.Test.Persistence
{
    [TestFixture]
    public class ProjectRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
            RemoveFile("sample.project");
            codebase = new CodeBase(CodeGraphFixture.Metropolis);
            projectRepository = new ProjectRepository();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveFile("sample.project");
        }

        private void RemoveFile(string testfile)
        {
            if (File.Exists(testfile))
                File.Delete(testfile);
        }

        private CodeBase codebase;
        private ProjectRepository projectRepository;

        [Test]
        public void Should_Load_Default_Project_From_JSON()
        {
            var project = projectRepository.LoadDefault();
            Assert.That(project, Is.Not.Null);
        }

        [Test]
        public void Should_Save_Project_To_JSON()
        {
            var filePath = Path.Combine(Environment.CurrentDirectory, "sample.project");
            projectRepository.Save(codebase, filePath);
            //manual verification, eventually should have string comparision
        }
    }
}