using System.Management.Automation.Runspaces;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        public void Invoke(string command, bool useNodePath)
        {

            using (var rs = RunspaceFactory.CreateRunspace())
            {
                rs.Open();
                var path = GetNodeBinPath(rs);
                if (useNodePath)
                    rs.CreatePipeline(path + command).Invoke();
                else
                    rs.CreatePipeline(command).Invoke();
            }
        }

        private static string GetNodeBinPath(Runspace rs)
        {
            var currentPath = rs.SessionStateProxy.Path.CurrentLocation.Path;
            // when installed via npm will show up under Desktop as current Path
            return currentPath.Contains("Desktop")
                ? @"..\AppData\Roaming\npm\node_modules\metropolis\node_modules\.bin\"
                : @"..\..\..\..\node_modules\.bin\";
            //this is for if using debug 
        }
    }
}