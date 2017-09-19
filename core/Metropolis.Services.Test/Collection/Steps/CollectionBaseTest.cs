using System;
using Metropolis.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Metropolis.Test.Api.Collection.Steps
{
    public abstract class CollectionBaseTest 
    {
        protected MetricsCommandArguments Args;
        protected MetricsResult Result;

#if DEBUG
        public const string NodeModulesPath =  @"../node_modules/.bin/";
#else
        public const string NodeModulesPath =  @"../node_modules/.bin/";
#endif

        [TestInitialize]
        public void SetUp()
        {
            Args = new MetricsCommandArguments
            {
                IgnoreFile = @"C:\ignore",
                ProjectName = "Test",
                MetricsOutputFolder = $"{Environment.CurrentDirectory}",
                SourceDirectory = @"C:\Source",
                RepositorySourceType = RepositorySourceType.ECMA
            };

            Result = new MetricsResult {ParseType = ParseType.SlocEcma, MetricsFile = @"C:\metrics_result.xml"};
        }
    }
}