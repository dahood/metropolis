using System;
using System.Collections.Generic;
using Metropolis.Api.Domain;
using Metropolis.Api.Parsers;
using Metropolis.Api.Parsers.CsvParsers;
using Metropolis.Api.Parsers.XmlParsers.CheckStyles;
using Metropolis.Api.Persistence;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services
{
    public class CodebaseService : ICodebaseService
    {
        private readonly ProjectRepository projectRepository = new ProjectRepository();

        private readonly Dictionary<ParseType, Func<IClassParser>> parseFactory = new Dictionary<ParseType, Func<IClassParser>>
        {
            {ParseType.VisualStudio, () => new VisualStudioMetricsParser()},
            {ParseType.RichardToxicity, () => new ToxicityParser()},
            {ParseType.PuppyCrawler, () => CheckStylesParser.PuppyCrawlParser},
            {ParseType.EsLint, () => CheckStylesParser.EslintParser},
            {ParseType.SlocJavaScript, () => new SourceLinesOfCodeParser(FileInclusion.Js)},
            {ParseType.SlocCSharp, () => new SourceLinesOfCodeParser(FileInclusion.CSharp)},
            {ParseType.SlocJava, () => new SourceLinesOfCodeParser(FileInclusion.Java)},
        };
        
        public void Save(CodeBase workspace, string fileName)
        {
            projectRepository.Save(workspace, fileName);
        }

        public CodeBase Load(string projectFileName)
        {
            return projectRepository.Load(projectFileName);
        }

        public CodeBase LoadDefault()
        {
            return projectRepository.LoadDefault();
        }

        public CodeBase GetToxicity(string fileName, string sourceBaseDirectory)
        {
            var result = new ToxicityParser().Parse(fileName);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase GetVisualStudioMetrics(string fileName, string sourceBaseDirectory)
        {
            var result = new VisualStudioMetricsParser().Parse(fileName);
            result.SourceType = RepositorySourceType.CSharp;
            return result;
        }

        public CodeBase Get(string filename, ParseType parseType)
        {
            return parseFactory[parseType]().Parse(filename);
        }
    }
}