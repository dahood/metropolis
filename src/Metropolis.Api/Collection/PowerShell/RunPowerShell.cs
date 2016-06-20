using System;
using System.Diagnostics;
using System.IO;
using System.Management.Automation.Runspaces;
using System.Text;
using Metropolis.Api.IO;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        private readonly IFileSystem fileSystem;

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
                try
                {
                    File.AppendAllText(Path.Combine(fileSystem.ProjectBuildFolder, "metropolis-error.log"), error.ToString());
                }
                catch (Exception e)
                {
                    //TODO: Deal with locked files
                    Debug.Write(e);
                    Debug.Write(error.ToString());
                }
            }
        }
    }
}