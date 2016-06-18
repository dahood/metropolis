using System;
using FluentAssertions;
using Metropolis.Api.Build;
using Metropolis.Common.Models;
using NUnit.Framework;
using NUnit.Framework.Api;

namespace Metropolis.Test.Api.Build
{
    [TestFixture]
    public class ProjectBuildFactoryTest
    {
        [Test]
        public void DotNet()
        {
            RunTest<DotNetProjectBuilder>(RepositorySourceType.CSharp);
        }

        [Test]
        public void FailsForUnknownType()
        {
            Assert.Throws<NotSupportedException>(() => new ProjectBuildFactory().BuilderFor(RepositorySourceType.Java));
        }

        private static void RunTest<T>(RepositorySourceType type) where T : IProjectBuilder
        {
            new ProjectBuildFactory().BuilderFor(type).Should().BeOfType<T>();
        }
    }
}
