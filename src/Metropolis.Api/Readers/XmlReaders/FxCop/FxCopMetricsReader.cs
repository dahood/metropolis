using System.IO;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Readers.XmlReaders.FxCop
{
    public class FxCopMetricsReader : IInstanceReader
    {
        private readonly IFxCopInstanceBuilder fxCopInstanceBuilder;

        public FxCopMetricsReader() : this (new FxCopInstanceBuilder())
        {
        }

        public FxCopMetricsReader(IFxCopInstanceBuilder fxCopInstanceBuilder)
        {
            this.fxCopInstanceBuilder = fxCopInstanceBuilder;
        }

        public CodeBase Parse(TextReader textReader)
        {
             return ParseXml(XElement.Load(textReader));
        }

        private CodeBase ParseXml(XElement xml)
        {
            var nameSpace = xml.GetDefaultNamespace();
            var instances = (from m in xml.Descendants(nameSpace + "Targets").Descendants(nameSpace + "Target")
                                        .Descendants(nameSpace + "Modules").Descendants(nameSpace + "Module")
                                        .Descendants(nameSpace + "Namespaces").Descendants(nameSpace + "Namespace")
                                        .Descendants(nameSpace + "Types").Descendants(nameSpace + "Type")
                             where m.Parent.Parent.AttributeValue("Name").IsNotEmpty() 
                             select fxCopInstanceBuilder.Build(m)).ToList();

            return new CodeBase("FxCop", new CodeGraph(instances), RepositorySourceType.CSharp);
        }
    }
}
    