using System;
using FluentAssertions;
using Metropolis.Common.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Common.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [Test]
        public void TrimTo()
        {
            @"c:\Clock.cs".TrimTo('.').Should().Be("cs");
        }
        [Test]
        public void TrimOff()
        {
            @"Analyze(MetricsCommandArguments) : CodeBase".TrimOff('(').Should().Be("Analyze");
        }

        [Test]
        public void FormatString()
        {
            "{0}-{1}-{2}".FormatWith("one", "2", "three").Should().Be("one-2-three");
        }

        [Test]
        public void AsInt()
        {
            "113".AsInt().Should().Be(113);
            Assert.Throws<FormatException>(() => "abs".AsInt()); //don't bury the exception.
        }

        [Test]
        public void TrimPath()
        {
            @"c:\folder1\folder2\Clock.cs".TrimPath("folder2").Should().Be(@"\folder2\Clock.cs");
            @"c:\folder1\folder2\Clock.cs".TrimPath("").Should().Be(@"c:\folder1\folder2\Clock.cs");
            @"c:\folder1\folder2\Clock.cs".TrimPath(null).Should().Be(@"c:\folder1\folder2\Clock.cs");
        }

        [Test]
        public void IsEmpty()
        {
            "".IsEmpty().Should().BeTrue();
            " ".IsEmpty().Should().BeTrue();
            ((string) null).IsEmpty().Should().BeTrue();

            "a".IsEmpty().Should().BeFalse();
            " a ".IsEmpty().Should().BeFalse();
            " abcdefg ".IsEmpty().Should().BeFalse();
        }

        [Test]
        public void IsNotEmpty()
        {
            "".IsNotEmpty().Should().BeFalse();
            " ".IsNotEmpty().Should().BeFalse();
            ((string)null).IsNotEmpty().Should().BeFalse();

            "a".IsNotEmpty().Should().BeTrue();
            " a ".IsNotEmpty().Should().BeTrue();
            " abcdefg ".IsNotEmpty().Should().BeTrue();
        }
    }
}
