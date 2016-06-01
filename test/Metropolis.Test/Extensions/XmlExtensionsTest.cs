using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Metropolis.Api.Extensions;
using NUnit.Framework;

namespace Metropolis.Test.Extensions
{
    [TestFixture]
    public class XmlExtensionsTest
    {
        [Test]
        public void AttributeValue()
        {
            var element = XDocument.Parse("<xml><class name='CodeBag'/></xml>");
            var found = (from m in element.Descendants()
                where m.HasAttribute("name")
                select m.AttributeValue("name")).ToList();
            found.Count.Should().Be(1);
            found.First().Should().Be("CodeBag");
        }

        [Test]
        public void HasAttribute()
        {
            var element = XDocument.Parse("<xml><class name='CodeBag'/></xml>");
            var found = (from m in element.Descendants()
                where m.HasAttribute("name")
                select m).ToList();
            found.Should().NotBeEmpty();
        }

        [Test]
        public void HasNoAttribute()
        {
            var element = XDocument.Parse("<xml><class kaka='CodeBag'/></xml>");
            var found = (from m in element.Descendants()
                where m.HasAttribute("name")
                select m).ToList();
            found.Should().BeEmpty();
        }
    }
}