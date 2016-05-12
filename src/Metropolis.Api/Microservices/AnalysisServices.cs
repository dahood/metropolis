using System;
using Metropolis.Api.Core.Domain;
using Metropolis.Common.Models;

namespace Metropolis.Api.Microservices
{
    public class AnalysisServices : IAnalysisService
    {
        private readonly INodeCommandFactory nodeCommandFactory;
        private readonly ICodebaseService codebaseService;

        public AnalysisServices() : this(new NodeCommandFactory(), new CodebaseService())
        {
            
        }

        public AnalysisServices(INodeCommandFactory nodeCommandFactory, ICodebaseService codebaseService)
        {
            this.nodeCommandFactory = nodeCommandFactory;
            this.codebaseService = codebaseService;
        }

        public CodeBase Analyze(ProjectDetails details)
        {
            var nodeCommand = nodeCommandFactory.CommandFor(details.RepositorySourcetype);
            nodeCommand.Run(details.SourceDirectory, details.MetricsOutputDirectory);

            throw new NotImplementedException();
        }
    }
}
