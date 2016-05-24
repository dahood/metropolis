using System.Windows;

namespace Metropolis.Views
{
    /// <summary>
    ///     Progress Log contains the log of all the work metro bot is doing to bring in metrics, calculate toxicity, etc
    ///     TODO: Create journal system to log what is happening to Log.Content
    /// </summary>
    public partial class ProgressLog
    {
        public ProgressLog()
        {
            InitializeComponent();
        }

        private void HideLog(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
        }
    }
}