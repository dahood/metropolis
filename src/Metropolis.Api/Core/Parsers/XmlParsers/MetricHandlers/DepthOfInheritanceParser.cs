using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.MetricHandlers
{
    public class DepthOfInheritanceParser : IJavaMetricParser
    {
        public int Order => 1;
        public string Id => "DIT";
        public void Parse(XElement metric, Dictionary<string, Class> classMap, XNamespace nameSpace)
        {
            metric.Descendants(nameSpace + "Values")
                  .Descendants(nameSpace + "Value")
                  .ForEach(each =>
                  {
                      var package = each.AttributeValue("package");
                      var name = each.AttributeValue("name");
                      var depthOfInheritance = each.AttributeValue("value").AsInt();
                      var found = classMap.FindOrCreate(each.AttributeValue("name"), () => new Class(package, name) {DepthOfInheritance = depthOfInheritance});

                      found.DepthOfInheritance = depthOfInheritance;
                  });
        }
    }
}
