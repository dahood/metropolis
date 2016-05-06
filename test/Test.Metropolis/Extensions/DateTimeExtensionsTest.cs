using System;
using FluentAssertions;
using Metropolis.Extensions;
using NUnit.Framework;

namespace Test.Metropolis.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTest
    {

        [Test]
        public void ShouldCompareTwoDates()
        {
            var date1 = new DateTime(2012, 01, 01, 01, 01, 01, 01);
            var date2 = new DateTime(2012, 01, 01, 01, 01, 01, 500);

            date1.EqualsWithin(date2, 500).Should().BeTrue();
        }

        [Test]
        public void ShouldCompareTwoDatesShouldNotBeSimilar()
        {
            var date1 = new DateTime(2012, 01, 01, 01, 01, 01, 01);
            var date2 = new DateTime(2012, 01, 01, 01, 01, 01, 500);

            date1.EqualsWithin(date2, 100).Should().BeFalse();
        }

        [Test]
        public void ShouldTruncateMilliseconds()
        {
            new DateTime(2012, 01, 01, 01, 01, 01, 01).TruncateMilliseconds().Millisecond.Should().Be(0);
        }
    }
}