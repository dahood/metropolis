namespace Metropolis.Api.IO
{
    public interface IUserPreferences
    {
        bool ShowTipOfTheDay { get; set; }
        string FxCopPath { get; set; }
        string MsBuildPath { get; set; }
    }
}
