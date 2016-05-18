using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation.Runspaces;
using Metropolis.Common.Models;

namespace Metropolis.Api.Services.Tasks.Commands
{
    public abstract class BaseCollectionStep : ICollectionStep
    {
        private readonly bool useNodePath;

        protected BaseCollectionStep(bool useNodePath)
        {
            this.useNodePath = useNodePath;
        }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = MetricResultFor(args);
            var command = PrepareCommand(args, result);
            SaveAndExecuteCommand(args, command);
            return new[] { result };
        }

        protected abstract string MetricsType { get; }
        protected abstract string Extension { get; }
        protected abstract ParseType ParseType { get; }
        protected abstract string PrepareCommand(MetricsCommandArguments args, MetricsResult result);

        private void SaveAndExecuteCommand(MetricsCommandArguments args, string command)
        {
            try
            {
                SaveMetricsCommand(args, command);
                InvokeCommand(command, useNodePath);
            }
            catch (Exception e)
            {
                //TODO: log this exception somewhere fancy 
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }

        private void SaveMetricsCommand(MetricsCommandArguments args, string cmd)
        {
            var fileName = Path.Combine(args.MetricsOutputDirectory, $"{args.ProjectName}_{ MetricsType}_command.ps1");
            File.WriteAllText(fileName, cmd);
        }

        private MetricsResult MetricResultFor(MetricsCommandArguments args)
        {
            return new MetricsResult { ParseType = ParseType, MetricsFile = GetOutputFile(args) };
        }

        private string GetOutputFile(MetricsCommandArguments args)
        {
            var fileName = $"{args.ProjectName}_{MetricsType}{Extension}".Replace(' ','_');
            return Path.Combine(args.MetricsOutputDirectory, fileName);
        }


        private static void InvokeCommand(string command, bool useNodePath)
        {
            using (var rs = RunspaceFactory.CreateRunspace())
            {
                rs.Open();
                var path = GetNodeBinPath(rs);
                if (useNodePath)
                    rs.CreatePipeline(path + command).Invoke();
                else
                    rs.CreatePipeline(command).Invoke();
            }
        }

        private static string GetNodeBinPath(Runspace rs)
        {
            var currentPath = rs.SessionStateProxy.Path.CurrentLocation.Path;
            // when installed via npm will show up under Desktop as current Path
            if (currentPath.Contains("Desktop"))
                return @"..\AppData\Roaming\npm\node_modules\metropolis-core\node_modules\.bin\";
            //this is for if using debug 
            return @"..\..\..\..\node_modules\.bin\";
        }
    }
}