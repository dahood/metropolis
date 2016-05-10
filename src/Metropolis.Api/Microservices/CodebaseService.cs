using System;
using System.Collections.Generic;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles;
using Metropolis.Api.Core.Persistence;
using Metropolis.Common;

namespace Metropolis.Api.Microservices
{
    public class CodebaseService : ICodebaseService
    {
        private readonly ProjectRepository projectRepository = new ProjectRepository();

        private readonly Dictionary<ParseType, Func<IClassParser>> parseFactory = new Dictionary<ParseType, Func<IClassParser>>
        {
            {ParseType.VisualStudio,    () => new VisualStudioMetricsParser()},
            {ParseType.RichardToxicity, () => new ToxicityParser()},
            {ParseType.PuppyCrawler,    () => CheckStylesParser.PuppyCrawlParser },
            {ParseType.EsLint,          () => CheckStylesParser.EslintParser },
            {ParseType.SlocJS,          () => new SourceLinesOfCodeParser(FileInclusion.Js) },
            {ParseType.SlocCS,          () => new SourceLinesOfCodeParser(FileInclusion.CSharp) },
            {ParseType.SlocJava,        () => new SourceLinesOfCodeParser(FileInclusion.Java) },
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

        public CodeBase Get(string filename, ParseType parseType, string sourceBaseDirectory = null)
        {
            return parseFactory[parseType]().Parse(filename);
        }
    }
}