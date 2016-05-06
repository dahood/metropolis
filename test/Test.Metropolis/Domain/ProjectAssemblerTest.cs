using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Domain;
using Metropolis.Utilities;
using NUnit.Framework;
using Test.Metropolis.Extensions;

namespace Test.Metropolis.Domain
{
    [TestFixture]
    public class ProjectAssemblerTest
    {
        private Class sourceClass;
        private Member sourceMember;
        private ClassVersionInfo sourceVersionInfo;

        [SetUp]
        public void SetUp()
        {
            Clock.Freeze();
            sourceVersionInfo = new ClassVersionInfo("info.txt", "commit");
            sourceMember = new Member("mbr", 1, 2, 3) {MissingDefaultCase = 4, NoFallthrough = 5, NumberOfParameters = 6};
            sourceClass = new Class("ns", "String", 1, 2, 3, 4, 5)
                                {
                                    Toxicity = 6,
                                    Meta = new [] {sourceVersionInfo},
                                    Members = new List<Member> {sourceMember}
                                };
        }

        [TearDown]
        public void TearDown()
        {
            Clock.Thaw();
        }

        [Test]
        public void CanSerializeCodeGraph()
        {
            var source = new CodeGraph(new [] {sourceClass});
            var project = ProjectAssembler.Assemble(source);

            project.Should().NotBeNull();

            var reconstructed = ProjectAssembler.Disassemble(project);
            reconstructed.ReflectionEquals(source, true).Should().BeTrue();
        }
    }
}
