using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Models;

namespace Metropolis.Api.Build
{
    public class DotNetProjectBuilder : IProjectBuilder
    {
        private IRunPowerShell powershell;

        public DotNetProjectBuilder() : this(new RunPowerShell())
        {
        }

        private DotNetProjectBuilder(IRunPowerShell runPowerShell)
        {
            powershell = runPowerShell;
        }

        public void Build(ProjectBuildArguments buildArgs)
        {
            throw new System.NotImplementedException();
        }
    }
}