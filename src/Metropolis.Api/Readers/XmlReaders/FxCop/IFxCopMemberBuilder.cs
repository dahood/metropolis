using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.FxCop
{
    public interface IFxCopMemberBuilder
    {
        Member Build(XElement xElement);
    }

    public class FxCopMemberBuilder : BaseFxCopBuilder, IFxCopMemberBuilder
    {
        public Member Build(XElement member)
        {
            var metrics = member.Descendants("Metrics").Descendants("Metric");
            return new Member(member.AttributeValue("Name").TrimTo('('),
                              GetMetricValue(metrics, "LinesOfCode"),
                              GetMetricValue(metrics, "CyclomaticComplexity"),
                              GetMetricValue(metrics, "ClassCoupling"));
        }
    }

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