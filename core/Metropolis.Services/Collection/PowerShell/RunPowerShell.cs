using System;
using System.Diagnostics;
using Metropolis.Common.Extensions;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        private ILogger _logger;
        private Boolean _isWindows;
        public RunPowerShell()
        {
            this._logger = LogManager.GetCurrentClassLogger("RunPowerShell");
            this._isWindows =  RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }
        private const int TimeOut = 60*1000; // 60,000 ms = 1 minute timeout

        public void Invoke(string command)
        {
            _logger.LogInformation("Running external command: " + command);
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = _isWindows ? "PowerShell.exe" : "sh",
                    Arguments = _isWindows ? $"-NoProfile -NonInteractive -Command \"{command};exit $LastExitCode\"" : $"-c \"{command}\"",
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
            var output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(TimeOut);

            _logger.LogInformation(output);

            var error = process.StandardError.ReadToEnd();
            if (error.IsNotEmpty())
            {
                throw new ApplicationException($"Run Powershell failed: {error}");
            }
        }
    }
}