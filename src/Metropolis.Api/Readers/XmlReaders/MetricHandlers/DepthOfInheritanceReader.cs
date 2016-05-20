using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.MetricHandlers
{
    public class DepthOfInheritanceReader : IJavaMetricReader
    {
        public int Order => 1;
        public string Id => "DIT";
        public void Parse(XElement metric, Dictionary<string, Instance> classMap, XNamespace nameSpace)
        {
            metric.Descendants(nameSpace + "Values")
                  .Descendants(nameSpace + "Value")
                  .ForEach(each =>
                  {
                      var package = each.AttributeValue("package");
                      var name = each.AttributeValue("name");
                      var depthOfInheritance = each.AttributeValue("value").AsInt();
                      var found = classMap.FindOrCreate(each.AttributeValue("name"), () => new Instance(package, name) {DepthOfInheritance = depthOfInheritance});

                      found.DepthOfInheritance = depthOfInheritance;
                  });
        }
    }
}
