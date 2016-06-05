using System.Diagnostics;
using System.Windows;
using Metropolis.TipOfTheDay;

namespace Metropolis.Views
{
    /// <summary>
    /// Interaction logic for TipOfTheDay.xaml
    /// </summary>
    public partial class TipOfTheDay : Window
    {
        public TipOfTheDay()
        {
            InitializeComponent();
            DataContext = new CameraControlTip();
        }

        private void OpenIssue(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/dahood/metropolis/issues");
        }

        private void OpenBeginnerGuide(object sender, RoutedEventArgs routedEventArgs)
        {
            Process.Start("https://github.com/dahood/metropolis/wiki/Beginner-Guide");
        }

        private void LoadCanvas(object sender, RoutedEventArgs e)
        {
            Canvas canvas = new Canvas();
            canvas.Show();
            this.Activate();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
