using System;
using System.Collections.Generic;
using System.IO;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Models;
using NLog;

namespace Metropolis.Api.Collection.Steps
{
    public abstract class BaseCollectionStep : ICollectionStep
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRunPowerShell powerShell;

        protected BaseCollectionStep(IRunPowerShell powerShell)
        {
            this.powerShell = powerShell;
        }

        public abstract string MetricsType { get; }
        public abstract string Extension { get; }
        public abstract ParseType ParseType { get; }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = MetricResultFor(args);
            var command = PrepareCommand(args, result);
            SaveAndExecuteCommand(args, command);
            return new[] {result};
        }

        public abstract string PrepareCommand(MetricsCommandArguments args, MetricsResult result);

        public static string LocateBinaries(string target)
        {
            return Locate(@"Collection\Binaries\", target);
        }

        public static string LocateSettings(string target)
        {
            return Locate(@"Collection\Settings\", target);
        }

        protected static string GetNodeBinPath()
        {
#if DEBUG
            return @"..\..\..\..\node_modules\.bin\";
#else
            return @"..\node_modules\.bin\";
#endif
        }

        private void SaveAndExecuteCommand(MetricsCommandArguments args, string command)
        {
            try
            {
                Logger.Info($"Command: {command} {args}");
                SaveMetricsCommand(args, command);
                InvokeCommand(command);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error occurred trying to exeucte an external process");
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }

        private void SaveMetricsCommand(MetricsCommandArguments args, string cmd)
        {
            var fileName = Path.Combine(args.MetricsOutputFolder, $"{args.ProjectName}_{MetricsType}_command.ps1");
            File.WriteAllText(fileName, cmd);
        }

        private MetricsResult MetricResultFor(MetricsCommandArguments args)
        {
            return new MetricsResult { ParseType = ParseType, MetricsFile = GetOutputFile(args) };
        }

        private string GetOutputFile(MetricsCommandArguments args)
        {
            var fileName = $"{args.ProjectName}_{MetricsType}{Extension}".Replace(' ', '_');
            return Path.Combine(args.MetricsOutputFolder, fileName);
        }


        private void InvokeCommand(string command)
        {
            powerShell.Invoke(command);
        }

        private static string Locate(string collectionPath, string target)
        {
            return Path.Combine(Environment.CurrentDirectory, collectionPath, target);
        }
    }
}