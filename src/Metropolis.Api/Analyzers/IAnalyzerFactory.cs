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

            return new JavaToxicityAnalyzer();
        }
    }
}