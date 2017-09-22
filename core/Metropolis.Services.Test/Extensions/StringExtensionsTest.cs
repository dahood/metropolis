using System;
using Metropolis.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using System.IO;

namespace Metropolis.Test.Common.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void TrimTo()
        {
            @"c:\Clock.cs".TrimTo('.').Should().Be("cs");
        }
        [TestMethod]
        public void TrimOff()
        {
            @"Analyze(MetricsCommandArguments) : CodeBase".TrimOff('(').Should().Be("Analyze");
        }

        [TestMethod]
        public void FormatString()
        {
            "{0}-{1}-{2}".FormatWith("one", "2", "three").Should().Be("one-2-three");
        }

        [TestMethod]
        public void AsInt()
        {
            "113".AsInt().Should().Be(113);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AsIntNotAcceptingNonInteger()
        {
            "abs".AsInt(); //don't bury the exception.
        }



        [TestMethod]
        public void TrimPath()
        {
            @"c:\folder1\folder2\Clock.cs".TrimPath("folder2").Should().Be(@"\folder2\Clock.cs");
            @"c:\folder1\folder2\Clock.cs".TrimPath("").Should().Be(@"c:\folder1\folder2\Clock.cs");
            @"c:\folder1\folder2\Clock.cs".TrimPath(null).Should().Be(@"c:\folder1\folder2\Clock.cs");
        }

        [TestMethod]
        public void IsEmpty()
        {
            "".IsEmpty().Should().BeTrue();
            " ".IsEmpty().Should().BeTrue();
            ((string) null).IsEmpty().Should().BeTrue();

            "a".IsEmpty().Should().BeFalse();
            " a ".IsEmpty().Should().BeFalse();
            " abcdefg ".IsEmpty().Should().BeFalse();
        }

        [TestMethod]
        public void IsNotEmpty()
        {
            "".IsNotEmpty().Should().BeFalse();
            " ".IsNotEmpty().Should().BeFalse();
            ((string)null).IsNotEmpty().Should().BeFalse();

            "a".IsNotEmpty().Should().BeTrue();
            " a ".IsNotEmpty().Should().BeTrue();
            " abcdefg ".IsNotEmpty().Should().BeTrue();
        }

        [TestMethod]
        public void ReplacePathSeperator_will_handle_windows()
        {
            var pathValue = "/usr/bin/anyvalue".ReplacePathSeperator();
            var containsOSPathSeperator = pathValue.Contains(Path.DirectorySeparatorChar.ToString());
            containsOSPathSeperator.Should().BeTrue();
                
        }
    }
}
