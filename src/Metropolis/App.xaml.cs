using System.Windows.Forms;
using Metropolis.Views;

namespace Metropolis
{
    /// <summary>
    /// Global Application 
    /// </summary>
    public partial class App
    {
        private static ProgressLog _progressLog;

        private App()
        {
            if (_progressLog != null)
                _progressLog = new ProgressLog();
        }

        public static void ShowLog()
        {
            _progressLog.Activate();
        }

        public static void HideLog()
        {
            _progressLog.Hide();
        }
    }
}