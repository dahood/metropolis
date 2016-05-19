using System.Windows;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    /// <summary>
    /// Metrobot is amazing... it can collect all the metrics you need from all the tools in the world....
    /// Currently the world is only Checkstyle, Eslint, and Sloc...but one day Metrobot will collect metrics for the entire world
    /// </summary>
    public partial class MetroBot 
    {
        public ProjectDetailsViewModel ProjectDetails { get; }

        public MetroBot()
        {
            InitializeComponent();
            ProjectDetails = new ProjectDetailsViewModel();
            DataContext = ProjectDetails;
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OnProceed(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
