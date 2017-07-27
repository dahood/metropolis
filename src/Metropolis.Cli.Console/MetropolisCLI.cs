using System;
using Metropolis.Api.Services;

namespace Metropolis.Console
{
    /// <summary>
    ///     metro.exe
    ///     The metro command line interface to Metropolis analysis for easy hook up to build agents like Teamcity
    ///     Export can be transformed by reporting by taking an output of JSON and a few prepared reports (html, with d3
    ///     perhaps?)
    /// </summary>
    internal class MetropolisCLI
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2)
                {
                    System.Console.WriteLine("hi there I'm Metropolis, i'm here to help you analyze code today...");

                    var configFile = args[0];
                    var projectFileLocation = args[1];

                    System.Console.WriteLine($"YAML Config File: {configFile} ");
                    System.Console.WriteLine($"Result Project File Folder: {projectFileLocation}");
                    var codeBase = new AnalysisServices().Analyze(configFile, projectFileLocation);
                    System.Console.Write(codeBase.ToString());
                }
                else
                {
                    System.Console.WriteLine("Metropolis expects you to have the following parameters: pathOfYamlConfigFile pathToProjectResultFile");
                    System.Console.WriteLine(@"eg: metro.exe c:\ProjectFolder\project.yml c:\ProjectFolder\Results");
                    Environment.Exit(1);
                }

                Environment.Exit(0);
            }
            catch (Exception e)
            {
                System.Console.Out.WriteLine(e.Message + e.StackTrace);
                Environment.Exit(1); // exit code for generic error
            }
        }
    }
}

