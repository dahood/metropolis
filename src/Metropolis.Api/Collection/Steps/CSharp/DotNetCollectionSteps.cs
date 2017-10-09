using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchiMetrics.Analysis;
using ArchiMetrics.Analysis.Common;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotNetCollectionSteps : CompositeCollectionStep
    {
        public DotNetCollectionSteps() : base(new ICollectionStep[] { new VisualStudioRebuildStep(), new VisualStudioCollectionStep(), new CpdCollectionStep(ParseType.CpdCsharp) }, false)
        {
        }
    }

    public class CSharpCollectionSteps : CompositeCollectionStep
    {
        public CSharpCollectionSteps() : base(new ICollectionStep[] { new ArchiMetricStep(), new CpdCollectionStep(ParseType.CpdCsharp) })
        {
        }

        public class ArchiMetricStep : ICollectionStep
        {
            public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
            {
                var solutionProvider = new SolutionProvider();
                var solution = solutionProvider.Get(args.ProjectFile).Result;
                var metricsCalculator = new CodeMetricsCalculator();
                var projects = solution.Projects.ToList();
                var calculateTasks = projects.Select(p => metricsCalculator.Calculate(p, solution));
                return Task.WhenAll(calculateTasks).Result.SelectMany(nm => nm).Select(x => new CSharpMetricsResult(x)).ToList();
            }

            public string ValidateMetricResults(string fileNametoValidate)
            {
                throw new System.NotImplementedException();
            }
        }
    }

}