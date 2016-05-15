using System;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Common.Models;

namespace Metropolis.Api.Analyzers
{
    public interface IAnalyzerFactory
    {
        ICodebaseAnalyzer For(RepositorySourceType repositorySourcetype);
    }

    public class AnalyzerFactory : IAnalyzerFactory
    {
        public ICodebaseAnalyzer For(RepositorySourceType sourceType)
        {
            if (sourceType == RepositorySourceType.CSharp)
                return new CSharpToxicityAnalyzer();
            if (sourceType == RepositorySourceType.ECMA)
                return new JavascriptToxicityAnalyzer();
            if (sourceType == RepositorySourceType.Java)
                return new JavaToxicityAnalyzer();

            throw new ApplicationException($"no Toxicity analyzer known for type: {sourceType}");
        }
    }
}