using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Readers.XmlReaders.CheckStyles
{
    public class CheckStylesReader : IInstanceReader
    {
        private readonly ICheckStylesClassBuilder classBuilder;

        public static CheckStylesReader EslintReader => new CheckStylesReader(new EsLintCheckStylesClassBuilder());
        public static CheckStylesReader PuppyCrawlReader => new CheckStylesReader(new PuppyCrawlCheckStylesClassBuilder());

        public CheckStylesReader(ICheckStylesClassBuilder classBuilder)
        {
            this.classBuilder = classBuilder;
        }

        public CodeBase Parse(TextReader textReader)
        {
            return ParseXml(XElement.Load(textReader));
        }

        private CodeBase ParseXml(XElement xml)
        {
            var nameSpace = xml.GetDefaultNamespace();
            var metrics = (from m in xml.Descendants(nameSpace + "file").Descendants(nameSpace + "error")
                           where m.AttributeValue("source").IsNotEmpty()
                           select BuildItem(m)).ToList();

            var classes = (from m in metrics
                           group m by m.FileFullName into cls
                           select classBuilder.Build(cls.Key, cls.ToList())).ToList();

            return new CodeBase(new CodeGraph(classes));
        }
        
        private static CheckStylesItem BuildItem(XElement node)
        {
            return new CheckStylesItem
            {
                FileFullName = node.Parent.AttributeValue("name"),
                Line = node.AttributeValue("line").AsInt(),
                Message = node.AttributeValue("message"),
                Source =  node.AttributeValue("source")
            };
        }

    }
}
