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
}