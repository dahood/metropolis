namespace Metropolis.Api.Collection.PowerShell
{
    /// <summary>
    /// Can throw exceptions (as command line arguments can be bad/wrong, etc) so please catch exceptions
    /// </summary>
    public interface IRunPowerShell
    {
        void Invoke(string command);
    }
}
