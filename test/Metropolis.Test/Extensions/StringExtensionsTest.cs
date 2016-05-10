using FluentAssertions;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void IsEmpty()
        {
            "".IsEmpty().Should().BeTrue();
            "k".IsEmpty().Should().BeFalse();
            ((string) null).IsEmpty().Should().BeTrue();
        }

        [Test]
        public void AsInt()
        {
            "1".AsInt().Should().Be(1);
        }

        [Test]
        public void FormatWith()
        {
            "kaka-{0}".FormatWith("poopoo").Should().Be("kaka-poopoo");
        }
    }
}