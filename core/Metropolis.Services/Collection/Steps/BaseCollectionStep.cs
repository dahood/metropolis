using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Metropolis.Api.Collection.PowerShell;
using Metropolis.Common.Models;
using Microsoft.Extensions.Logging;


namespace Metropolis.Api.Collection.Steps
{
    public abstract class BaseCollectionStep : ICollectionStep
    {
        private readonly IRunPowerShell powerShell;
        private ILogger _logger;

        protected BaseCollectionStep(IRunPowerShell powerShell)
        {
            this.powerShell = powerShell;
            this._logger = LogManager.GetCurrentClassLogger("BaseCollectionStep");
        }

        public abstract string MetricsType { get; }
        public abstract string Extension { get; }
        public abstract ParseType ParseType { get; }

        public IEnumerable<MetricsResult> Run(MetricsCommandArguments args)
        {
            var result = MetricResultFor(args);
            var command = PrepareCommand(args, result);
            SaveAndExecuteCommand(args, command);
            var validationResult = ValidateMetricResults(GetOutputFile(args));
            if (validationResult != string.Empty)
            {
                _logger.LogError("Step Validation Error for " + validationResult);
                throw new ApplicationException("Step Validation Error for " + validationResult);
            }
            return new[] {result};
        }

        public abstract string ValidateMetricResults(string fileNametoValidate);

        public abstract string PrepareCommand(MetricsCommandArguments args, MetricsResult result);

        public static string LocateBinaries(string target)
        {
            return Locate(@"Collection/Binaries/".Replace('/', Path.DirectorySeparatorChar), target);
        }

        public static string LocateSettings(string target)
        {
            return Locate(@"Collection/Settings/".Replace('/', Path.DirectorySeparatorChar), target);
        }

        protected static string GetNodeBinPath()
        {
            string nodePath;
#if DEBUG
            nodePath = @"../../node_modules/.bin/";
#else
            nodePath = @"../node_modules/.bin/";
#endif
            return nodePath.Replace('/', Path.DirectorySeparatorChar);
        }

        private void SaveAndExecuteCommand(MetricsCommandArguments args, string command)
        {
            try
            {
                _logger.LogInformation($"Command: {command} {args}");
                SaveMetricsCommand(args, command);
                InvokeCommand(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred trying to exeucte an external process");
                throw new ApplicationException("Error occurred trying to exeucte an external process", e);
            }
        }

        private void SaveMetricsCommand(MetricsCommandArguments args, string cmd)
        {
            var fileName = Path.Combine(args.MetricsOutputFolder, $"{args.ProjectName}_{MetricsType}_command.sh");
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
            
            #if DEBUG
                return Path.Combine("bin/Debug/netcoreapp2.0".Replace('/', Path.DirectorySeparatorChar), collectionPath, target);
            #else
                return Path.Combine(collectionPath, target);
            #endif
        }
    }
}