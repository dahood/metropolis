using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotNetCpdCollectionStep : CompositeCollectionStep
    {
        public DotNetCpdCollectionStep() : base(new ICollectionStep[] { new DotProjectCleanStep(),  new CpdCollectionStep(ParseType.CpdCsharp)}, false)
        {
        }
    }
}