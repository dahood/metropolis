using System;
using System.Diagnostics;
using Metropolis.Common.Extensions;
using NLog;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private RunPowerShell()
        {
        }

        public void Invoke(string command)
        {
            try
            {
                var process = new Process { 
                    StartInfo =  new ProcessStartInfo
                                {
                                    FileName = "PowerShell.exe",
                                    Arguments = command,
                                    CreateNoWindow = true,
                                    ErrorDialog = false,
                                    UseShellExecute = false,
                                    WindowStyle = ProcessWindowStyle.Hidden,
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true
                                }
                    };
                process.Start();
                if (Logger.IsDebugEnabled)
                    Logger.Debug(process.StandardOutput.ReadToEnd());
                
                var error = process.StandardError.ReadToEnd();
                if (error.IsNotEmpty())
                    Logger.Error(error);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}