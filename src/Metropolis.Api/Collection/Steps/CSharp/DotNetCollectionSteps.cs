namespace Metropolis.Api.Collection.Steps.CSharp
{
    public class DotNetCollectionSteps : CompositeCollectionStep
    {
        public DotNetCollectionSteps() : base(new ICollectionStep[] { new VisualStudioCollectionStep(), new DotNetCpdCollectionStep() }, true)
        {
        }
    }
}