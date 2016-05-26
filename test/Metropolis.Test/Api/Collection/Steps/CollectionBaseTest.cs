using System;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps
{
    public abstract class CollectionBaseTest : StrictMockBaseTest
    {
        protected MetricsCommandArguments Args;
        protected MetricsResult Result;

#if DEBUG
        public const string NodeModulesPath =  @"..\..\..\..\node_modules\.bin\";
#else
        public const string NodeModulesPath =  @"..\node_modules\.bin\";
#endif

        [SetUp]
        public void SetUp()
        {
            Args = new MetricsCommandArguments
            {
                IgnoreFile = @"C:\ignore",
                ProjectName = "Test",
                MetricsOutputDirectory = $"{Environment.CurrentDirectory}",
                SourceDirectory = @"C:\Source",
                RepositorySourceType = RepositorySourceType.ECMA
            };

            Result = new MetricsResult {ParseType = ParseType.SlocEcma, MetricsFile = @"C:\metrics_result.xml"};
        }
    }
}