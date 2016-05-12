using FluentAssertions;
using Metropolis.Test.Extensions;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Services
{
    public abstract class StrictMockBaseTest
    {
        private MockRepository mock;

        [SetUp]
        protected virtual void SetupMockRepository()
        {
            mock = new MockRepository(MockBehavior.Strict);
        }

        [TearDown]
        protected virtual void VerifyMocks()
        {
            mock.VerifyAll();
        }

        protected Mock<T> CreateMock<T>() where T : class
        {
            return mock.Create<T>();
        }

        protected Mock<T> CreateMock<T>(params object[] args) where T : class
        {
            return mock.Create<T>(args);
        }

        protected static bool Matches<T>(T actual, T expected)
        {
            actual.Should().NotBeNull();
            expected.Should().NotBeNull();
            actual.ReflectionEquals(expected).Should().BeTrue();
            return true;
        }
    }
}