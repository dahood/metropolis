using Metropolis.Properties;

namespace Metropolis
{
    public interface IUserPreferences
    {
        bool ShowTipOfTheDay { get; set; }
    }

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
