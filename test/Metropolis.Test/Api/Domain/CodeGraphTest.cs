using FluentAssertions;
using Metropolis.Api.Domain;
using Metropolis.Test.Fixtures;
using NUnit.Framework;

namespace Metropolis.Test.Api.Domain
{
    [TestFixture]
    public class CodeGraphTest
    {
        private CodeBase codeBase;

        [SetUp]
        public void Setup()
        {
            codeBase = CodeGraphFixture.Metropolis;
        }

        [Test]
        public void Should_Report_CodeBase_Attributes_Simple_Case()
        {
            codeBase.LinesOfCode().Should().Be(100);
            codeBase.InstanceCount().Should().Be(2);
            codeBase.AverageToxicity().Should().Be(0d);
            codeBase.Density().Should().Be(50d);
            codeBase.Duplicates().Should().Be(0.02d);
        }
        [Test]
        public void Should_Report_CodeBase_Attributes_Duplicates_VeryHigh()
        {
            var instance = codeBase.AllInstances[0];
            instance.Duplicates.Add(new Duplicate(50,1, instance.PhysicalPath));

            codeBase.LinesOfCode().Should().Be(100);
            codeBase.Duplicates().Should().Be(0.52d);
        }
    }
}