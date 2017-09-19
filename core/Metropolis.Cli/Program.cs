using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using NLog.Extensions.Logging;
using Metropolis.Api.Services;

namespace Metropolis.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Metropolis expects you to have the following parameters: pathOfYamlConfigFile pathToProjectResultFile");
                    Console.WriteLine(@"eg: metro.exe c:\ProjectFolder\project.yml c:\ProjectFolder\Results");
                    Environment.Exit(1);
                }

                var loggerFactory = new LoggerFactory();
                loggerFactory.ConfigureNLog("nlog.config");
                loggerFactory.AddNLog();
                LogManager.LoggerFactory = loggerFactory;
                var logger = loggerFactory.CreateLogger<Program>();
                

                var configFile = args[0];
                var projectFileLocation = args[1];
                Console.WriteLine("hi there I'm Metropolis, i'm here to help you analyze code today...");

                var codeBase = new AnalysisServicesCore().Analyze(configFile, projectFileLocation);
                logger.LogInformation("===========================================");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}
