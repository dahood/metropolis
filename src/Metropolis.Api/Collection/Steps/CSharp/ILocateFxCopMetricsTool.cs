using Metropolis.Api.Extensions;
using Metropolis.Api.Utilities;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface ILocateFxCopMetricsTool
    {
        string FxCopMetricsToolPath { get; }
    }

    public class LocateFxCopMetricsTool : ILocateFxCopMetricsTool
    {
        public const string ExpectedPath = @"\Program Files (x86)\Microsoft Visual Studio 14.0\Team Tools\Static Analysis Tools\FxCop\metrics.exe";

        private readonly IFileSystem fileSystem;

        public LocateFxCopMetricsTool() : this(new FileSystem())
        {
        }

        public LocateFxCopMetricsTool(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public string FxCopMetricsToolPath
        {
            get
            {
                foreach (var driveInfo in fileSystem.AllDrives)
                {
                    var potentialPath = $"{driveInfo.Name}{ExpectedPath}";
                    if (fileSystem.FileExists(potentialPath))
                        return potentialPath;
                }
                return string.Empty;
            }
        }
    }
}
