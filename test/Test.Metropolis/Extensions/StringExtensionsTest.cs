using FluentAssertions;
using Metropolis.Extensions;
using NUnit.Framework;

namespace Test.Metropolis.Extensions
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