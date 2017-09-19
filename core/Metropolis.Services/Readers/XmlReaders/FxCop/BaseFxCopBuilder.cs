using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.FxCop
{
    public abstract class BaseFxCopBuilder
    {
        protected int GetMetricValue(IEnumerable<XElement> elements, string name)
        {
            var defaultElement = new XElement(name);
            defaultElement.SetAttributeValue("Value", "0");
            var found = elements.FirstOrDefault(x => x.Attribute("Name").Value == name);
            return (found ?? defaultElement).AttributeValue("Value").Replace(",","").AsInt();
        }
    }
}