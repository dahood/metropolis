using System.Windows;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    /// <summary>
    /// Used to gather information so that automation can collect and import metrics into Metropolis
    /// </summary>
    public partial class GatherAnalysisWizard 
    {
        public ProjectDetailsViewModel ProjectDetails { get; }

        public GatherAnalysisWizard()
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
