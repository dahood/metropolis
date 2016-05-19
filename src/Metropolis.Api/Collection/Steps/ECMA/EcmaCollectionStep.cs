using System.Collections.Generic;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.ECMA
{
    public class EcmaCollectionStep : CompositeCollectionStep
    {
        public EcmaCollectionStep() : base(new List<ICollectionStep> {new EsLintCollectionStep(), new SlocCollectionStep(ParseType.SlocEcma) }, true)
        {
        }
    }
}