using System;
using System.Collections.Generic;
using Metropolis.Api.Parsers.CsvParsers;
using Metropolis.Api.Parsers.XmlParsers.CheckStyles;
using Metropolis.Common.Models;

namespace Metropolis.Api.Parsers
{
    public class MetricsParserFactory : IMetricsParserFactory
    {
        private readonly Dictionary<ParseType, Func<IClassParser>> parseFactory = new Dictionary<ParseType, Func<IClassParser>>
        {
            {ParseType.VisualStudio, () => new VisualStudioMetricsParser()},
            {ParseType.RichardToxicity, () => new ToxicityParser()},
            {ParseType.PuppyCrawler, () => CheckStylesParser.PuppyCrawlParser},
            {ParseType.EsLint, () => CheckStylesParser.EslintParser},
            {ParseType.SlocEcma, () => new SourceLinesOfCodeParser(FileInclusion.Js)},
            {ParseType.SlocCSharp, () => new SourceLinesOfCodeParser(FileInclusion.CSharp)},
            {ParseType.SlocJava, () => new SourceLinesOfCodeParser(FileInclusion.Java)},
        };

        public IClassParser ParserFor(ParseType parseType)
        {
            if (!parseFactory.ContainsKey(parseType))
                throw new ApplicationException($"{parseType} is not a known metrics parser type");
            return parseFactory[parseType]();
        }
    }
}