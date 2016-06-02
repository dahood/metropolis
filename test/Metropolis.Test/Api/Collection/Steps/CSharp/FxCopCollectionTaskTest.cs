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
        private Mock<ILocateFxCopMetricsTool> locateFxCopMetricsTool;
        private readonly MetricsCommandArguments args = new MetricsCommandArguments
                                                            {
                                                                MetricsOutputFolder = @"c:\metrics",
                                                                ProjectName = "test",
                                                                RepositorySourceType = RepositorySourceType.CSharp,
                                                                SourceDirectory = @"c:\source"
                                                            };

        private const string DllName = "mydll.dll";
        private const string fxCopMetricsPath = @"C:\FxcopFolder\metrics.exe";
        private readonly MetricsResult expectedResult = new MetricsResult {
                                                                ParseType = ParseType.FxCop,
                                                                MetricsFile = @"c:\metrics\test_mydll_metrics.xml"
                                                        };
        

        [SetUp]
        public void SetUp()
        {
            powerShell = CreateMock<IRunPowerShell>();
            fileSystem = CreateMock<IFileSystem>();
            locateFxCopMetricsTool = CreateMock<ILocateFxCopMetricsTool>();
            locateFxCopMetricsTool.Setup(x => x.FxCopMetricsToolPath).Returns(fxCopMetricsPath);

            task = new FxCopCollectionTask(powerShell.Object, fileSystem.Object, locateFxCopMetricsTool.Object);
        }

        [Test]
        public void CanRunCollectionTask()
        {
            var expectedCommand = FxCopCollectionTask.CommandTemplate.FormatWith(fxCopMetricsPath, DllName, expectedResult.MetricsFile);
            fileSystem.Setup(x => x.GetFileName(DllName)).Returns("mydll");
            powerShell.Setup(x => x.Invoke(expectedCommand));
            var result = task.Run(args, DllName);

            Validate.Begin()
                    .IsNotNull(result, "Result").Check()
                    .IsEqual(result.MetricsFile, expectedResult.MetricsFile, "MetricsFile")
                    .IsEqual(result.ParseType, expectedResult.ParseType, "ParseType")
                    .Check();
        }
    }
}
