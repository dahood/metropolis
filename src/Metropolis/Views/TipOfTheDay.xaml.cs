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
        private readonly ITipOfTheDayFactory tipTheDayFactory;

        public TipOfTheDay()
        {
            InitializeComponent();

            //TODO:  change this when we plug in IOC
            tipTheDayFactory = new TipOfTheDayFactory();
            NextTip(this, new RoutedEventArgs());
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
            var canvas = new Canvas();
            canvas.Show();
            Activate();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NextTip(object sender, RoutedEventArgs e)
        {
            DataContext = tipTheDayFactory.Next;
        }
    }
}
