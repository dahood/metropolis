using System.Management.Automation.Runspaces;

namespace Metropolis.Api.Collection.PowerShell
{
    public class RunPowerShell : IRunPowerShell
    {
        public void Invoke(string command)
        {

            using (var rs = RunspaceFactory.CreateRunspace())
            {
                rs.Open();
                rs.CreatePipeline(command).Invoke();
            }
        }
    }
}