﻿using System.Collections.Generic;
using Metropolis.Api.Collection.Steps;
using Metropolis.Api.Collection.Steps.AllLanguages;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.CSharp
{
    [TestFixture]
    public class DotNetCollectionStepsTest : BaseCompositeCollectionStepTest<DotNetCollectionSteps>
    {
        protected override IEnumerable<ICollectionStep> ExpectedSteps => 
            new ICollectionStep[] {new VisualStudioRebuildStep(), new VisualStudioCollectionStep(), new CpdCollectionStep(ParseType.CpdCsharp), };
    }
}
