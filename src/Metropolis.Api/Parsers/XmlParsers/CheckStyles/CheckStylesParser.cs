using System;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;

namespace Metropolis.Api.Parsers.XmlParsers.CheckStyles
{
    public class CheckStylesParser : IClassParser
    {
        private readonly ICheckStylesClassBuilder classBuilder;

        public static CheckStylesParser EslintParser => new CheckStylesParser(new EsLintCheckStylesClassBuilder());
        public static CheckStylesParser PuppyCrawlParser => new CheckStylesParser(new PuppyCrawlCheckStylesClassBuilder());

        public CheckStylesParser(ICheckStylesClassBuilder classBuilder)
        {
            this.classBuilder = classBuilder;
        }

        public CodeBase Parse(string fileName)
        {
            return ParseXml(XElement.Load(fileName));
        }

        private CodeBase ParseXml(XElement xml)
        {
            var nameSpace = xml.GetDefaultNamespace();
            var metrics = (from m in xml.Descendants(nameSpace + "file").Descendants(nameSpace + "error")
                           where m.AttributeValue("source").IsNotEmpty()
                           where m.HasAttribute("column")
                           select BuildItem(m)).ToList();

            var classes = (from m in metrics
                           group m by m.Name into cls
                           select classBuilder.Build(cls.Key, cls.ToList())).ToList();

            if (classBuilder is PuppyCrawlCheckStylesClassBuilder)
                return new JavaToxicityAnalyzer().Analyze(classes);
            if (classBuilder is EsLintCheckStylesClassBuilder)
                return new JavascriptToxicityAnalyzer().Analyze(classes);

            throw new ApplicationException("No analyzer setup for this type of code");
        }
        
        private static CheckStylesItem BuildItem(XElement node)
        {
            return new CheckStylesItem
            {
                Name = node.Parent.AttributeValue("name"),
                Line = node.AttributeValue("line").AsInt(),
                Column = node.AttributeValue("column").AsInt(),
                Message = node.AttributeValue("message"),
                Source =  node.AttributeValue("source")
            };
        }

    }

    public class CheckStylesItem
    {
        public string Name { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
    }
}
