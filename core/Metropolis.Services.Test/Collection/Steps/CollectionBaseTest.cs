using System;
using Metropolis.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Metropolis.Test.Api.Collection.Steps
{
    public abstract class CollectionBaseTest 
    {
        protected MetricsCommandArguments Args;
        protected MetricsResult Result;

        public string NodeModulesPath {get; private set;} 

        [TestInitialize]
        public void SetUp()
        {
            NodeModulesPath =  "../node_modules/.bin/".Replace('/', Path.DirectorySeparatorChar);

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