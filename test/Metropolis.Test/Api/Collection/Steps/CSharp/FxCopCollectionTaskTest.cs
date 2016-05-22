using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.Collection.Steps.CSharp;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.Test.Api.Services;
using Metropolis.Test.Utilities;
using Moq;
using NUnit.Framework;

namespace Metropolis.Test.Api.Collection.Steps.CSharp
{
    [TestFixture]
    public class FxCopCollectionTaskTest : StrictMockBaseTest
    {
        private FxCopCollectionTask task;
        private Mock<IRunPowerShell> powerShell;
        private Mock<IFileSystem> fileSystem;
        private readonly MetricsCommandArguments args = new MetricsCommandArguments
                                                            {
                                                                MetricsOutputDirectory = @"c:\metrics",
                                                                ProjectName = "test",
                                                                RepositorySourceType = RepositorySourceType.FxCop,
                                                                SourceDirectory = @"c:\source"
                                                            };

        private const string DllName = "mydll.dll";
        private readonly MetricsResult expectedResult = new MetricsResult {
                                                                ParseType = ParseType.FxCop,
                                                                MetricsFile = @"c:\metrics\test_mydll_metrics.xml"
                                                        };


        [SetUp]
        public void SetUp()
        {
            powerShell = CreateMock<IRunPowerShell>();
            fileSystem = CreateMock<IFileSystem>();

            task = new FxCopCollectionTask(powerShell.Object, fileSystem.Object);
        }

        [Test]
        public void CanRunCOllectionTask()
        {
            var expectedCommand = FxCopCollectionTask.CommandTemplate.FormatWith(DllName, expectedResult.MetricsFile);
            fileSystem.Setup(x => x.GetFileName(DllName)).Returns("mydll");
            powerShell.Setup(x => x.Invoke(expectedCommand, false));
            var result = task.Run(args, DllName);

            Validate.Begin()
                    .IsNotNull(result, "Result").Check()
                    .IsEqual(result.MetricsFile, expectedResult.MetricsFile, "MetricsFile")
                    .IsEqual(result.ParseType, expectedResult.ParseType, "ParseType")
                    .Check();
        }
    }
}
