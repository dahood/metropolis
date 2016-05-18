using System.Collections.Generic;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps
{
    public class EcmaCollectionStep : CompositeCollectionStep
    {
        public EcmaCollectionStep() : base(new List<ICollectionStep> {new EsLintCollectionStep(), new SlocCollectionStep(ParseType.SlocEcma) }, true)
        {
        }
    }
}