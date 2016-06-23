using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation.Runspaces;
using System.Text;
using Metropolis.Api.IO;
using NLog;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        private readonly IFileSystem fileSystem;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public RunPowerShell() : this(new FileSystem()) { }

        private RunPowerShell(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Invoke(string command)
        {

            using (var rs = RunspaceFactory.CreateRunspace())
            {
                rs.Open();

                var pipeline = rs.CreatePipeline(command);
                pipeline.Invoke();
                if (pipeline.Error.Count <= 0) return;
                var error = new StringBuilder();
                while (!pipeline.Error.EndOfPipeline)
                {
                    error.AppendLine(pipeline.Error.Read().ToString());
                }
                Logger.Error(error);
            }
        }
    }
}