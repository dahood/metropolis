using System.Collections.Generic;
using FluentAssertions;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Utilities;
using Metropolis.Test.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Domain
{
    [TestFixture]
    public class ProjectAssemblerTest
    {
        [SetUp]
        public void SetUp()
        {
            Clock.Freeze();
            sourceVersionInfo = new InstanceVersionInfo("info.txt", "commit");
            sourceMember = new Member("mbr", 1, 2, 3)
            {
                MissingDefaultCase = 4,
                NoFallthrough = 5,
                NumberOfParameters = 6
            };
            sourceInstance = new Instance("ns", "String", 1, 2, 3, 4, 5)
            {
                Toxicity = 6,
                Meta = new[] {sourceVersionInfo},
                Members = new List<Member> {sourceMember}
            };
        }

        [TearDown]
        public void TearDown()
        {
            Clock.Thaw();
        }

        private Instance sourceInstance;
        private Member sourceMember;
        private InstanceVersionInfo sourceVersionInfo;

        [Test]
        public void CanSerializeCodeGraph()
        {
            var source = new CodeGraph(new[] {sourceInstance});
            var project = ProjectAssembler.Assemble(source);

            project.Should().NotBeNull();

            var reconstructed = ProjectAssembler.Disassemble(project);
            reconstructed.ReflectionEquals(source, true).Should().BeTrue();
        }
    }
}