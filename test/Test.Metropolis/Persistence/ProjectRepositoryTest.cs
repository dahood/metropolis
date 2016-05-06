using System;
using System.IO;
using Metropolis.Domain;
using Metropolis.Persistence;
using NUnit.Framework;
using Test.Metropolis.Fixtures;

namespace Test.Metropolis.Persistence
{
    [TestFixture]
    public class ProjectRepositoryTest
    {
        [SetUp]
        public void Setup()
        {
            codebase = new CodeBase(CodeGraphFixture.Metropolis);
            projectRepository = new ProjectRepository();
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