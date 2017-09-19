using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;
using Microsoft.Extensions.Logging;

namespace Metropolis.Api.Collection.Steps.ECMA
{
    public class EcmaCollectionStep : CompositeCollectionStep
    {
        public EcmaCollectionStep() : base(new ICollectionStep[] { new EsLintCollectionStep(), new SlocCollectionStep(ParseType.SlocEcma),
                new CpdCollectionStep(ParseType.CpdEcma) }, true) {}
    }
}