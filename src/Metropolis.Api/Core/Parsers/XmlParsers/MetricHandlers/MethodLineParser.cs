using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Metropolis.Domain;
using Metropolis.Extensions;

namespace Metropolis.Parsers.XmlParsers.MetricHandlers
{
    public class MethodLineParser : IJavaMetricParser
    {
        public int Order => 2;
        public string Id => "MLOC";

        public void Parse(XElement metric, Dictionary<string, Class> classMap, XNamespace nameSpace)
        {
            metric.Descendants(nameSpace + "Values")
                  .Descendants(nameSpace + "Value")
                  .ForEach(each =>
                  {
                      var className = each.AttributeValue("source").Replace(".java","").Replace(".java","");
                      var methodName = each.AttributeValue("name");
                      var linesOfCode = each.AttributeValue("value").AsInt();

                      classMap.DoWhenItemFound(className, item =>
                      {
                          if (item.Members.All(x => x.Name != methodName))
                              item.AddMember(new [] {new Member(methodName, linesOfCode, 0, 0) });
                      });
                  });
        }
    }
}
