using System;
using System.Collections.Generic;
using Metropolis.Api.Parsers.CsvParsers;
using Metropolis.Api.Parsers.XmlReaders.CheckStyles;
using Metropolis.Common.Models;

namespace Metropolis.Api.Parsers
{
    public class MetricsParserFactory : IMetricsParserFactory
    {
        private readonly Dictionary<ParseType, Func<IInstanceReader>> parseFactory = new Dictionary<ParseType, Func<IInstanceReader>>
        {
            {ParseType.VisualStudio, () => new VisualStudioMetricsReader()},
            {ParseType.RichardToxicity, () => new ToxicityReader()},
            {ParseType.PuppyCrawler, () => CheckStylesReader.PuppyCrawlReader},
            {ParseType.EsLint, () => CheckStylesReader.EslintReader},
            {ParseType.SlocEcma, () => new SourceLinesOfCodeReader(FileInclusion.Js)},
            {ParseType.SlocCSharp, () => new SourceLinesOfCodeReader(FileInclusion.CSharp)},
            {ParseType.SlocJava, () => new SourceLinesOfCodeReader(FileInclusion.Java)},
        };

        public IInstanceReader ParserFor(ParseType parseType)
        {
            if (!parseFactory.ContainsKey(parseType))
                throw new ApplicationException($"{parseType} is not a known metrics parser type");
            return parseFactory[parseType]();
        }
    }
}