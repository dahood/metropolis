using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
