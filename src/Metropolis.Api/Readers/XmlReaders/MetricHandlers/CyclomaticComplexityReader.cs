using System.Collections.Generic;
using System.Xml.Linq;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.MetricHandlers
{
    public class CyclomaticComplexityReader : IJavaMetricReader
    {
        public int Order => 4;
        public string Id => "VG";

        public void Parse(XElement metric, Dictionary<string, Instance> classMap, XNamespace nameSpace)
        {
            metric.Descendants(nameSpace + "Values")
                  .Descendants(nameSpace + "Value")
                  .ForEach(each =>
                  {
                      var className = each.AttributeValue("source").Replace(".java", "").Replace(".java", "");
                      var methodName = each.AttributeValue("name");
                      var cyclomaticComplexity = each.AttributeValue("value").AsInt();

                      classMap.DoWhenItemFound(className, item => item.GetMemberByName(methodName).CylomaticComplexity = cyclomaticComplexity);
                  });
        }
    }
}
