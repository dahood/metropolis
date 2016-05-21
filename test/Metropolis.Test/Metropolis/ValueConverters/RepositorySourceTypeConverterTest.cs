using System.Globalization;
using FluentAssertions;
using Metropolis.Common.Models;
using Metropolis.ValueConverters;
using NUnit.Framework;

namespace Metropolis.Test.Metropolis.ValueConverters
{
    [TestFixture]
    public class RepositorySourceTypeConverterTest
    {
        private RepositorySourceTypeConverter converter;

        [SetUp]
        public void SetUp()
        {
            converter = new RepositorySourceTypeConverter();
        }

        [Test]
        public void Convert_CSharp()
        {
            converter.Convert(RepositorySourceType.CSharp, typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be("CSharp");
        }

        [Test]
        public void Convert_Java()
        {
            converter.Convert(RepositorySourceType.Java, typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be("Java");
        }

        [Test]
        public void Convert_ECMA()
        {
            converter.Convert(RepositorySourceType.ECMA, typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be("ECMA");
        }

        [Test]
        public void ConvertBack_CSharp()
        {
            converter.ConvertBack("CSharp", typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be(RepositorySourceType.CSharp);
        }

        [Test]
        public void ConvertBack_Java()
        {
            converter.ConvertBack("Java", typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be(RepositorySourceType.Java);
        }

        [Test]
        public void ConvertBack_ECMA()
        {
            converter.ConvertBack("ECMA", typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().Be(RepositorySourceType.ECMA);
        }

        [Test]
        public void Convert_WhenNotCorrectType()
        {
            converter.Convert("not of correct type", typeof (RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().BeNull();
        }

        [Test]
        public void Convert_When_NullOrEmpty()
        {
            converter.Convert(null, typeof (RepositorySourceType), null, CultureInfo.CurrentCulture).Should().BeNull();
            converter.Convert(string.Empty, typeof (RepositorySourceType), null, CultureInfo.CurrentCulture).Should().BeNull();
        }

        [Test]
        public void ConvertBack_WhenValueNull()
        {
            converter.ConvertBack(null, typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().BeNull();
        }

        [Test]
        public void ConvertBack_WhenValueEmpty()
        {
            converter.ConvertBack("", typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().BeNull();
            converter.ConvertBack(" ", typeof(RepositorySourceType), null, CultureInfo.CurrentCulture)
                     .Should().BeNull();
        }

    }
}
