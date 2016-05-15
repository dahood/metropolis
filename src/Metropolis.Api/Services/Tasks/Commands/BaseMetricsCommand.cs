using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Runspaces;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public abstract class BaseMetricsCommand : IMetricsCommand
    {
        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = MetricResultFor(args);
            var command = PrepareCommand(args, result);
            SaveAndExecuteCommand(args, command);
            return new[] { result };
        }

        public abstract string MetricsType { get; }
        public abstract string Extension { get; }
        protected abstract string PrepareCommand(MetricsCommandArguments args, MetricsResult result);
        protected abstract MetricsResult MetricResultFor(MetricsCommandArguments args);
        
        protected void SaveAndExecuteCommand(MetricsCommandArguments args, string command)
        {
            try
            {
                SaveMetricsCommand(args, command);
                InvokeCommand(command);
            }
            catch (Exception e)
            {
                //TODO: log this exception somewhere fancy 
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }
        private static void InvokeCommand(string command)
        {
            var rsf = RunspaceFactory.CreateRunspace();
            rsf.Open();
            rsf.CreatePipeline(command).Invoke();
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