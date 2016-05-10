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

        public static bool HasAttribute(this XElement element, string name)
        {
            return element.Attributes().Any(x => x.Name == name);
        }
    }
}
