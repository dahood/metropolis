namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotNetCollectionSteps : CompositeCollectionStep
    {
        public DotNetCollectionSteps() : base(new ICollectionStep[] { new VisualStudioCollectionStep()}, true) //new DotNetCpdCollectionStep() }, true)
        {
        }
    }
}