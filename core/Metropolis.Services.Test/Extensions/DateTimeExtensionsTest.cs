using System;
using FluentAssertions;
using Metropolis.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metropolis.Test.Extensions
{
    [TestClass]
    public class DateTimeExtensionsTest
    {
        [TestMethod]
        public void ShouldCompareTwoDates()
        {
            var date1 = new DateTime(2012, 01, 01, 01, 01, 01, 01);
            var date2 = new DateTime(2012, 01, 01, 01, 01, 01, 500);

            date1.EqualsWithin(date2, 500).Should().BeTrue();
        }

        [TestMethod]
        public void ShouldCompareTwoDatesShouldNotBeSimilar()
        {
            var date1 = new DateTime(2012, 01, 01, 01, 01, 01, 01);
            var date2 = new DateTime(2012, 01, 01, 01, 01, 01, 500);

            date1.EqualsWithin(date2, 100).Should().BeFalse();
        }

        [TestMethod]
        public void ShouldTruncateMilliseconds()
        {
            new DateTime(2012, 01, 01, 01, 01, 01, 01).TruncateMilliseconds().Millisecond.Should().Be(0);
        }
    }
}