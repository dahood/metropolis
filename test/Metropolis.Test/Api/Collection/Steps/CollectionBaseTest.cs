using Metropolis.Common.Models;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps
{
    public abstract class CollectionBaseTest
    {
        protected MetricsCommandArguments Args;
        protected MetricsResult Result;

        [SetUp]
        public void SetUp()
        {
            Args = new MetricsCommandArguments
            {
                IgnorePath = @"C:\ignore",
                ProjectName = "Test",
                MetricsOutputDirectory = @"c:\output",
                SourceDirectory = @"C:\Source",
                RepositorySourceType = RepositorySourceType.ECMA
            };

            Result = new MetricsResult {ParseType = ParseType.SlocEcma, MetricsFile = @"C:\metrics_result.xml"};
        }
    }
}