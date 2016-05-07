namespace Metropolis.Cli.Console
{
    /// <summary>
    /// Command line interface to hook up to build agents like Teamcity
    /// </summary>
    class Metropolis
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
        }
    }
}
