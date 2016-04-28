using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Domain;
using Metropolis.Extensions;
using Metropolis.Parsers.XmlParsers.MetricHandlers;
using Metropolis.Analyzers.Toxicity;

namespace Metropolis.Parsers.XmlParsers
{
    public class XmlClassParser : IClassParser
    {
        private readonly IEnumerable<IJavaMetricParser> parsers;

        private static readonly IEnumerable<IJavaMetricParser> MetricParsers = new List<IJavaMetricParser>
            { new DepthOfInheritanceParser(), new MethodLineParser(), new ClassAttributeParser(), new CyclomaticComplexityParser()};

        public XmlClassParser() : this(MetricParsers)
        {
        }

        private XmlClassParser(IEnumerable<IJavaMetricParser> javaMetricHandlers)
        {
            parsers = javaMetricHandlers;
        }

        public CodeBase Parse(string fileName, string sourceBaseDirectory = "")
        {
            return ParseText(File.ReadAllText(fileName));
        }

        public CodeBase ParseText(string text)
        {
            var elements = XElement.Parse(text);
            var nameSpace = elements.GetDefaultNamespace();
            var classMap  = new Dictionary<string, Class>();

            var toDoList = (from m in elements.Elements(nameSpace + "Metric")
                            join p in parsers on m.Attribute("id").Value equals p.Id
                            select new {Metric = m, Parser = p}).ToList().OrderBy(x => x.Parser.Order);

            toDoList.ForEach(task => task.Parser.Parse(task.Metric, classMap, nameSpace));

            var toxicityAnalyzer = new JavaToxicityAnalyzer();

            return toxicityAnalyzer.Analyze(classMap.Values.ToList());
        }
    }
}
