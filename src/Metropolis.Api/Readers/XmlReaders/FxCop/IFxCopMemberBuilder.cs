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
            return new Member(member.AttributeValue("Name").TrimOff('('),
                              GetMetricValue(metrics, "LinesOfCode"),
                              GetMetricValue(metrics, "CyclomaticComplexity"),
                              GetMetricValue(metrics, "ClassCoupling"));
        }
    }
}