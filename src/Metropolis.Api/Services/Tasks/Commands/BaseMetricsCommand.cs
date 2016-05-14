using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management.Automation.Runspaces;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public abstract class BaseMetricsCommand : IMetricsCommand
    {
        public abstract IEnumerable<MetricsResult> Run(MetricsCommandArguments args);
        public abstract string MetricsType { get; }
        public abstract string Extension { get; }

        protected void SaveAndExecuteCommand(MetricsCommandArguments args, string command)
        {
            SaveMetricsCommand(args, command);
            try
            {
                var rsf = RunspaceFactory.CreateRunspace();
                rsf.Open();
                var pipeline = rsf.CreatePipeline(command);
                pipeline.Invoke();
            } catch (Exception e)
            {
                //TODO: log this exception somewhere fancy 
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }

        protected void SaveMetricsCommand(MetricsCommandArguments args, string cmd)
        {
            var fileName = Path.Combine(args.MetricsOutputDirectory, $"{args.ProjectName}_{ MetricsType}_command.ps1");
            File.WriteAllText(fileName, cmd);
        }

        protected string GetMetricsOutoutFile(MetricsCommandArguments args)
        {
            var fileName = $"{args.ProjectName}_{MetricsType}{Extension}".Replace(' ','_');
            return Path.Combine(args.MetricsOutputDirectory, fileName);
        }
    }
}