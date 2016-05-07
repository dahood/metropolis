using System;

namespace Metropolis.Console
{
    /// <summary>
    /// metro.exe
    /// 
    /// The metro command line interface to Metropolis analysis for easy hook up to build agents like Teamcity
    /// 
    /// Export can be transformed by reporting by taking an output of JSON and a few prepared reports (html, with d3 perhaps?)
    /// </summary>
    class MetropolisCLI
    {
        static void Main(string[] args)
        {

            try
            {
                if (args.Length == 0)
                {
                    System.Console.WriteLine("hi there I'm Metropolis, i'm here to help you analyze code today...");
                    System.Console.WriteLine("Enter Directory:");
                    var directory = System.Console.ReadLine();
                    System.Console.Write("Enter Source Type (java,javascript,csharp, default:csharp):");
                    var sourceType = System.Console.ReadLine();

                    //TODO: hook into Metropolis.API
                }
                System.Console.Write(@"Metropolis v0.0.1 - Command Usage: metro.exe csharp c:\dev\metropolis");
                //TODO: hook this into Metropolis.API

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
