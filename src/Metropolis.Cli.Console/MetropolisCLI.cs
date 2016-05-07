using System;

namespace Metropolis.Cli.Console
{
    /// <summary>
    /// The metro command line interface to Metropolis analysis for easy hook up to build agents like Teamcity
    /// 
    /// Export can be transformed by reporting by taking an output of JSON and a few prepared reports (html, with d3 perhaps?)
    /// </summary>
    class MetropolisCLI
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            { 
            System.Console.Write("hi there I'm metropolis");
            System.Console.Write("Enter Directory:");
            var directory = System.Console.ReadLine();
            System.Console.Write("Enter Source Type (java,javascript,csharp, default:csharp):");
            var sourceType = System.Console.ReadLine();
            }
            System.Console.Write("Metropolis v0.0.1 - Command Usage: metropolis.exe -d c:\dev\metropolis -s csharp");
            //TODO: hook this into Metropolis.API

            Environment.Exit(0);
        }
    }
}
