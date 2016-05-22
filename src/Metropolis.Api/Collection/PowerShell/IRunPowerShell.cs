namespace Metropolis.Api.Collection.PowerShell
{
    public interface IRunPowerShell
    {
        void Invoke(string command, bool useNodePath);
    }
}
