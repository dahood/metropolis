using FluentAssertions;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
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

        [Test]
        public void IsEmpty()
        {
            "".IsEmpty().Should().BeTrue();
            "k".IsEmpty().Should().BeFalse();
            ((string) null).IsEmpty().Should().BeTrue();
        }

        [Test]
        public void TrimTo()
        {
            "abcdef".TrimTo('d').Should().Be("ef");
        }

        [Test]
        public void TrimPath()
        {
            @"c:\Folder1\Folder2\Folder3".TrimPath("Folder2").Should().Be(@"\Folder2\Folder3");
        }
    }
}