using System;

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
                Console.WriteLine("hi there I'm Metropolis, i'm here to help you analyze code today...");

                var configFile = args[0];
                var projectFileLocation = args[1];

                Console.WriteLine($"YAML Config File: {configFile} ");
                Console.WriteLine($"Result Project File Folder: {projectFileLocation}");
                //var codeBase = new AnalysisServices().Analyze(configFile, projectFileLocation);
                //Console.Write(codeBase.ToString());
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
                Environment.Exit(1);
            }
        }
    }
}
