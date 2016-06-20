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
    }
}