using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Parsers.XmlReaders.MetricHandlers;

namespace Metropolis.Api.Parsers.XmlReaders
{
    public class XmlInstanceReader : IInstanceReader
    {
        private readonly IEnumerable<IJavaMetricReader> parsers;

        private static readonly IEnumerable<IJavaMetricReader> MetricParsers = new List<IJavaMetricReader>
            { new DepthOfInheritanceReader(), new MethodLineReader(), new ClassAttributeReader(), new CyclomaticComplexityReader()};

        public XmlInstanceReader() : this(MetricParsers)
        {
        }

        private XmlInstanceReader(IEnumerable<IJavaMetricReader> javaMetricHandlers)
        {
            parsers = javaMetricHandlers;
        }

        public CodeBase Parse(string fileName)
        {
            return ParseText(File.ReadAllText(fileName));
        }

        public CodeBase ParseText(string text)
        {
            var elements = XElement.Parse(text);
            var nameSpace = elements.GetDefaultNamespace();
            var classMap  = new Dictionary<string, Instance>();

            var toDoList = (from m in elements.Elements(nameSpace + "Metric")
                            join p in parsers on m.Attribute("id").Value equals p.Id
                            select new {Metric = m, Parser = p}).ToList().OrderBy(x => x.Parser.Order);

            toDoList.ForEach(task => task.Parser.Parse(task.Metric, classMap, nameSpace));

            var toxicityAnalyzer = new JavaToxicityAnalyzer();

            return toxicityAnalyzer.Analyze(classMap.Values.ToList());
        }
    }
}
