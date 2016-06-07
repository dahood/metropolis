using System.Collections.Generic;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.Java
{
    public class JavaCollectionStep : CompositeCollectionStep
    {
        public JavaCollectionStep()
            : base(new List<ICollectionStep> {new PuppyCrawlerCheckstyleCollectionStep(), new SlocCollectionStep(ParseType.SlocJava)}, true)
        {
        }
    }
}