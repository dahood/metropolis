using Metropolis.Api.IO.AutoSave;
using Metropolis.Api.Properties;

namespace Metropolis.Api.IO
{
    public class UserPreferences : IUserPreferences
    {
        public bool ShowTipOfTheDay
        {
            get { return (bool) Settings.Default["ShowTips"]; }
            set
            {
                Settings.Default["ShowTips"] = value;
                Settings.Default.Save();
            }
        }

        public string FxCopPath
        {
            get { return (string) Settings.Default["FxCopPath"]; }
            set
            {
                Settings.Default["FxCopPath"] = value;
                Settings.Default.Save();
            }
        }

        public string MsBuildPath
        {
            get { return (string) Settings.Default["MsBuildPath"]; }

            set
            {
                Settings.Default["MsBuildPath"] = value;
                Settings.Default.Save();
            }
        }
    }
}