using System.Linq;
using System.Xml.Linq;
using FluentAssertions;
using Metropolis.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metropolis.Test.Extensions
{
    [TestClass]
    public class XmlExtensionsTest
    {
        [TestMethod]
        public void AttributeValue()
        {
            var element = XDocument.Parse("<xml><class name='CodeBag'/></xml>");
            var found = (from m in element.Descendants()
                where m.HasAttribute("name")
                select m.AttributeValue("name")).ToList();
            found.Count.Should().Be(1);
            found.First().Should().Be("CodeBag");
        }

        [TestMethod]
        public void HasAttribute()
        {
            var element = XDocument.Parse("<xml><class name='CodeBag'/></xml>");
            var found = (from m in element.Descendants()
                where m.HasAttribute("name")
                select m).ToList();
            found.Should().NotBeEmpty();
        }

        [TestMethod]
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