using FluentAssertions;
using Metropolis.Api.Analyzers;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Analyzers
{
    [TestFixture]
    public class AnalyzeFactoryTest
    {
        private AnalyzerFactory factory;

        [SetUp]
        public void SetUp()
        {
            factory = new AnalyzerFactory();
        }

        [Test]
        public void CSharp()
        {
            factory.For(RepositorySourceType.CSharp)
                .Should().NotBeNull()
                .And.BeAssignableTo<CSharpToxicityAnalyzer>();
        }

        [Test]
        public void Java()
        {
            factory.For(RepositorySourceType.Java)
                .Should().NotBeNull()
                .And.BeAssignableTo<JavaToxicityAnalyzer>();
        }

        [Test]
        public void Ecma()
        {
            factory.For(RepositorySourceType.ECMA)
                .Should().NotBeNull()
                .And.BeAssignableTo<JavascriptToxicityAnalyzer>();
        }
    }
}
