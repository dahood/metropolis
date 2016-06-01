using System;
using FluentAssertions;
using Metropolis.Api.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Utilities
{
    [TestFixture]
    public class DateTimeExtensionTests
    {
        DateTime today = Clock.Today;
        DateTime tomorrow = Clock.Today.AddDays(1);
        [Test]
        public void Max()
        {
            today.Max(tomorrow).Should().Be(tomorrow);
            tomorrow.Max(today).Should().Be(tomorrow);
        }

        [Test]
        public void Min()
        {
            today.Min(tomorrow).Should().Be(today);
            tomorrow.Min(today).Should().Be(today);
        }

        [Test]
        public void StartOfMonth()
        {
            var expected = new DateTime(tomorrow.Year, tomorrow.Month, 1);
            tomorrow.StartOfMonth().Should().Be(expected);
        }
        [Test]
        public void EndOfMonth()
        {
            var days = DateTime.DaysInMonth(today.Year, today.Month);
            var expected = new DateTime(today.Year, today.Month, days, 23, 59, 59, 999);

            today.EndOfMonth().Should().Be(expected);
        }

        [Test]
        public void MakeEarly()
        {
            var expected = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0, 0);
            today.MakeEarly().Should().Be(expected);
        }

        [Test]
        public void MakeLate()
        {
            var expected = new DateTime(today.Year, today.Month, today.Day, 23, 59, 59, 999);
            today.MakeLate().Should().Be(expected);
        }
    }
}
