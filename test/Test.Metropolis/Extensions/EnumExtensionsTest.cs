using System;
using FluentAssertions;
using Metropolis.Extensions;
using NUnit.Framework;
using Descr = System.ComponentModel.DescriptionAttribute;

namespace Test.Metropolis.Extensions
{
    public enum FavoriteSport
    {
        [Descr("Puck Bunnies")]
        [EnumOrder(Order = 2)]
        Hockey = 1,

        [Descr("Convicts")]
        [EnumOrder(Order = 1)]
        Football = 2,

        [Descr("Roid Rage")]
        [EnumOrder(Order = 4)]
        Baseball = 3,

        [EnumOrder(Order = 3)]
        Soccer
    };

    [TestFixture]
    public class EnumExtensionsTest
    {
        [Test]
        public void IntValue()
        {
            FavoriteSport.Hockey.IntValue().Should().Be(1);
        }

        [Test]
        public void ToEnum()
        {
            1.ToEnum<FavoriteSport>().Should().Be(FavoriteSport.Hockey);
            1L.ToEnum<FavoriteSport>().Should().Be(FavoriteSport.Hockey);
            "Hockey".ToEnum<FavoriteSport>().Should().Be(FavoriteSport.Hockey);
        }

        [Test]
        public void ToEnumExact_String()
        {
            var sports = "Hockey".ToEnumExact<FavoriteSport>();
            sports.Should().Be(FavoriteSport.Hockey);
        }

        [Test]
        public void ToEnumExact_ShouldFail()
        {
            Assert.Throws<ArgumentException>(() => "kaka".ToEnumExact<FavoriteSport>());
        }

        [Test]
        public void ToEnumExact_String_Collection()
        {
            var sports = new[] {"Hockey", "Soccer"}.ToEnumExact<FavoriteSport>();
            sports.Should().Contain(new[] {FavoriteSport.Hockey, FavoriteSport.Soccer});
        }

        [Test]
        public void ToEnumExactOrDefault()
        {
            "kaka".ToEnumExactOrDefault(FavoriteSport.Baseball)
                  .Should().Be(FavoriteSport.Baseball);

            "Football".ToEnumExactOrDefault(FavoriteSport.Baseball)
                  .Should().Be(FavoriteSport.Football);
        }

        [Test]
        public void ToNullableEnumExact()
        {
            "".ToNullableEnumExact<FavoriteSport>().Should().BeNull();

            "Hockey".ToNullableEnumExact<FavoriteSport>().Should().Be(FavoriteSport.Hockey);
        }

        [Test]
        public void ToEnumByDescription()
        {
            "Puck Bunnies".ToEnumByDescription<FavoriteSport>().Should().Be(FavoriteSport.Hockey);
            "Roid Rage".ToEnumByDescription<FavoriteSport>().Should().Be(FavoriteSport.Baseball);
        }

        [Test]
        public void ToEnumByDescription_Missing()
        {
            Assert.Throws<ArgumentException>(() => "kaka".ToEnumByDescription<FavoriteSport>());
        }

        [Test]
        public void GetDescription()
        {
            FavoriteSport.Hockey.GetDescription().Should().Be("Puck Bunnies");
            Assert.Throws<ArgumentException>(() => 1.GetDescription());
        }

        [Test]
        public void GetEnumList()
        {
            new[] {"Hockey","Football"}.ToEnumList<FavoriteSport>()
                .Should().Contain(new[] {FavoriteSport.Hockey, FavoriteSport.Football});
        }

        [Test]
        public void ToEnumList()
        {
            EnumExtensions.GetEnumList<FavoriteSport>()
                .Should().Contain(new[] {FavoriteSport.Hockey, FavoriteSport.Baseball, FavoriteSport.Football, FavoriteSport.Soccer});
        }

        [Test]
        public void GetSortedList()
        {
            EnumExtensions.GetSortedList<FavoriteSport>()
                .Should().Contain(new[] {FavoriteSport.Football, FavoriteSport.Hockey, FavoriteSport.Soccer, FavoriteSport.Baseball});
        }

        [Test]
        public void GetSortOrder()
        {
            FavoriteSport.Hockey.GetSortOrder().Should().Be(2);
            FavoriteSport.Football.GetSortOrder().Should().Be(1);
        }

        [Test]
        public void GetName()
        {
            FavoriteSport.Hockey.GetName<FavoriteSport>().Should().Be("Hockey");
        }
    }
}