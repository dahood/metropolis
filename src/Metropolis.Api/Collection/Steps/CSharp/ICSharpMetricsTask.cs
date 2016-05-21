using System;
using System.IO;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Api.Utilities;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.Api.Collection.Steps.CSharp
{
    public interface ICSharpMetricsTask
    {
        MetricsResult Run(MetricsCommandArguments args, string each);
    }

    public class CSharpMetricsTask : ICSharpMetricsTask
    {
        public const string CommandTemplate = @"&'C:\Program Files (x86)\Microsoft Visual Studio 14.0\Team Tools\Static Analysis Tools\FxCop\FxCopCmd.exe'/f:'{0}' /o:'{1}' ";

        private readonly IRunPowerShell powerShell;
        private readonly IFileSystem fileSystem;

        public CSharpMetricsTask() : this(new RunPowerShell(), new FileSystem())
        {
        }

        public CSharpMetricsTask(IRunPowerShell powerShell, IFileSystem fileSystem)
        {
            this.powerShell = powerShell;
            this.fileSystem = fileSystem;
        }

        public MetricsResult Run(MetricsCommandArguments args, string targetdll)
        {
            var result = new MetricsResult{ ParseType = ParseType.FxCop, MetricsFile = GetMetricsOutputFileName(args, targetdll) };
            var command = CommandTemplate.FormatWith(targetdll, result.MetricsFile);

            powerShell.Invoke(command, false);
            return result;
        }

        private string GetMetricsOutputFileName(MetricsCommandArguments args, string targetdll)
        {
            var assemblyName = fileSystem.GetFileName(targetdll).Replace(".dll", string.Empty);
            return Path.Combine(args.MetricsOutputDirectory, $"{args.ProjectName}_{assemblyName}_metrics.dll");
        }
    }
}