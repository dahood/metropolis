using System.Linq;
using System.Xml.Linq;

namespace Metropolis.Api.Extensions
{
    public static class XmlExtensions
    {
        public static string AttributeValue(this XElement element, string name)
        {
            return element.Attribute(name).Value;
        }

        public static XElement GetFxCopTypeDescendant(this XElement element, string name)
        {
            return element.Descendants("Metrics").Descendants("Metric").First(x => x.AttributeValue("Name") == name);
        }

        public static bool HasAttribute(this XElement element, string name)
        {
            return element.Attributes().Any(x => x.Name == name);
        }
    }
}
