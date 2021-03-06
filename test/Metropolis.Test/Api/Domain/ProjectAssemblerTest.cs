﻿using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Api.Persistence;
using Metropolis.Api.Utilities;
using Metropolis.Common.Models;
using Metropolis.Test.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Api.Domain
{
    [TestFixture]
    public class ProjectAssemblerTest
    {
        private Instance sourceInstance;
        private Member sourceMember;

        [SetUp]
        public void SetUp()
        {
            Clock.Freeze();
            sourceMember = new Member("mbr", 1, 2, 3)
            {
                MissingDefaultCase = 4,
                NoFallthrough = 5,
                NumberOfParameters = 6
            };
            var codeBag = new CodeBag("ns", CodeBagType.Package, @"c:\dev\src\java");
            sourceInstance = new Instance(codeBag, "String", new Location(@"C:\dev\src\java\something.java"))
            {
                Toxicity = 6,
                Members = new List<Member> {sourceMember}
            };
        }

        [TearDown]
        public void TearDown()
        {
            Clock.Thaw();
        }

        [Test, Ignore("currently the serialization stuff will be busted until we work out the domain a bit better...please stand by")]
        public void CanSerializeCodeGraph()
        {
            var source = new CodeBase(new CodeGraph(new[] {sourceInstance}))
            {
                RunDate = Clock.Today,
                ProjectFile = @"c:\proj.file",
                ProjectFolder = @"c:\projFolder\",
                Name = "MyProject",
                IgnoreFile = @"c:\myIgnoreFile.metropolisIgnore",
                SourceType = RepositorySourceType.CSharp,
                SourceBaseDirectory = @"c:\sourceFolder\"
            };

            var project = ProjectRepository.Get(source);

            project.Should().NotBeNull();

            var reconstructed = ProjectRepository.Get(project);
            reconstructed.ReflectionEquals(source, true).Should().BeTrue();
        }
    }
}