using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Metropolis.Extensions;
using NUnit.Framework;

namespace Test.Metropolis.Extensions
{
    [TestFixture]
    public class XmlExtensionsTest
    {
        [Test]
        public void AttributeValue()
        {
            var element = XDocument.Parse("<xml><class name='Name'/></xml>");
            var found = (from m in element.Descendants()
                         where m.HasAttribute("name")
                         select m.AttributeValue("name")).ToList();
            found.Count.Should().Be(1);
            found.First().Should().Be("Name");
        }
        [Test]
        public void HasAttribute()
        {
            var element = XDocument.Parse("<xml><class name='Name'/></xml>");
            var found = (from m in element.Descendants()
                         where m.HasAttribute("name")
                         select m).ToList();
            found.Should().NotBeEmpty();
        }

        [Test]
        public void HasNoAttribute()
        {
            var element = XDocument.Parse("<xml><class kaka='Name'/></xml>");
            var found = (from m in element.Descendants()
                         where m.HasAttribute("name")
                         select m).ToList();
            found.Should().BeEmpty();
        }
    }
}
