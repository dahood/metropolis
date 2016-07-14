using System;
using System.Diagnostics;
using Metropolis.Common.Extensions;
using NLog;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private const int TimeOut = 60*1000; // 60,000 ms = 1 minute timeout

        public void Invoke(string command)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "PowerShell.exe",
                    Arguments = $"-NoProfile -NonInteractive -Command \"{command};exit $LastExitCode\"",
                    CreateNoWindow = true,
                    ErrorDialog = false,
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            process.Start();
            //TODO: Used Exited event instead of blocking this thread
            // https://msdn.microsoft.com/en-us/library/system.diagnostics.process.exited(v=vs.110).aspx
            process.WaitForExit(TimeOut);

            if (Logger.IsDebugEnabled)
            {
                var readToEnd = process.StandardOutput.ReadToEnd();
                if (readToEnd.IsNotEmpty())
                    Logger.Debug(readToEnd);
            }

            var error = process.StandardError.ReadToEnd();
            if (error.IsNotEmpty())
            {
                throw new ApplicationException($"Run Powershell failed: {error}");
            }
        }
    }
}