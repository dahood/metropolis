using System;
using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class CSharpVisualStudioCollectionStep : ICollectionStep
    {
        const  string commandTemplate = @"&'C:\Program Files (x86)\Microsoft Visual Studio 14.0\Team Tools\Static Analysis Tools\FxCop\FxCopCmd.exe'/f:'{0}'  /o:'{1}' ";
        const string rulesScriptletTemplate = @"/r:'{0}' ";
        
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            throw new NotImplementedException();
        }
    }
}