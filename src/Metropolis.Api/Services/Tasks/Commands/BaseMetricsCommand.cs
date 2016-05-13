using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public abstract class BaseMetricsCommand : IMetricsCommand
    {
        public abstract IEnumerable<MetricsResult> Run(MetricsCommandArguments args);
        public abstract string MetricsType { get; }
        public abstract string Extension { get; }

        protected void ExecuteProcess(string command)
        {
            try
            {
                var process = new Process {StartInfo = new ProcessStartInfo(command)};
                process.Start();
                process.WaitForExit();
            } catch (Exception e)
            {
                //TODO: log this exception somewhere fancy 
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }

        protected string GetMetricsOutoutFile(MetricsCommandArguments args)
        {
            var fileName = $"{args.ProjectName}_{MetricsType}.{Extension}";
            return Path.Combine(args.MetricsOutputDirectory, fileName);
        }
    }
}