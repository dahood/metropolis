using System.IO;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.FxCop
{
    public class FxCopInstanceBuilder : BaseFxCopBuilder, IFxCopInstanceBuilder
    {
        private readonly IFxCopMemberBuilder fxCopMemberBuilder;

        public FxCopInstanceBuilder() : this(new FxCopMemberBuilder())
        {
        }

        public FxCopInstanceBuilder(IFxCopMemberBuilder fxCopMemberBuilder)
        {
            this.fxCopMemberBuilder = fxCopMemberBuilder;
        }

        public Instance Build(XElement typeElement)
        {
            var members = (from m in typeElement.Descendants("Members").Descendants("Member")
                select fxCopMemberBuilder.Build(m)).ToList();
            var nspace = typeElement.Parent.Parent.AttributeValue("Name");
            var metrics = typeElement.Descendants("Metrics").Descendants("Metric");
            var physicalfile = GetPhysicaFileFrom(typeElement);
            var codeBag = new CodeBag(nspace, CodeBagType.Namespace, Path.GetDirectoryName(physicalfile));

            return InstanceBuilder.Build(codeBag, typeElement.AttributeValue("Name"), physicalfile,
                GetMetricValue(metrics, "LinesOfCode"),
                GetMetricValue(metrics, "CyclomaticComplexity"),
                GetMetricValue(metrics, "DepthOfInheritance"),
                GetMetricValue(metrics, "ClassCoupling"),
                members);
        }

        private static string GetPhysicaFileFrom(XElement typeElement)
        {
            var member = (from m in typeElement.Descendants("Members").Descendants("Member")
                where m.HasAttribute("File")
                select m).FirstOrDefault();
            return member?.AttributeValue("File");
        }
    }
}