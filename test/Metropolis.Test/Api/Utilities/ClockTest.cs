using System;
using System.Threading;
using FluentAssertions;
using Metropolis.Api.Utilities;
using NUnit.Framework;

namespace Metropolis.Test.Api.Utilities
{
    [TestFixture]
    public class ClockTest
    {
        [SetUp]
        public void SetUp()
        {
            Clock.Thaw();
        }

        [TearDown]
        public void TearDown()
        {
            Clock.Thaw();
        }

        [Test]
        public void ShouldGetFrozenDateTimeIfClockIsFrozen()
        {
            Clock.Freeze();
            var clockNow = Clock.Now;
            Thread.Sleep(100);
            clockNow.Should().Be(Clock.Now);
        }

        [Test]
        public void ShouldGetFrozenDateTimeIfClockIsFrozenToASpecificTime()
        {
            var frozenTime = new DateTime(1971, 08, 01);
            Clock.Freeze(frozenTime);
            Assert.That(frozenTime, Is.EqualTo(Clock.Now));
            Thread.Sleep(100);
            frozenTime.Should().Be(Clock.Now);
        }

        [Test]
        public void ShouldHaveMaxDateTime()
        {
            Clock.MaxDateTime.Should().Be(DateTime.MaxValue);
        }

        [Test]
        public void ShouldHaveMinDateTime()
        {
            Clock.MinDateTime.Should().Be(DateTime.MinValue);
        }

        [Test]
        public void ShouldSetBackToSystemDateTimeAfterClockIsUnfrozen()
        {
            Clock.Freeze();
            var clockFrozen = Clock.Now;
            Thread.Sleep(100);
            clockFrozen.Should().Be(Clock.Now);
            Clock.Thaw();
            Clock.Now.Should().Be(DateTime.Now);
        }

        [Test]
        public void EndOfToday()
        {
            var expected = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999);
            Clock.EndOfToday.Should().Be(expected);
        }

        [Test]
        public void Today()
        {
            var expected = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            Clock.Today.Should().Be(expected);
        }
    }
}
