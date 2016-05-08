using System.Windows;
using Metropolis.Api.Models;

namespace Metropolis.Views
{
    /// <summary>
    /// Interaction logic for GatherAnalysisWizard.xaml
    /// </summary>
    public partial class GatherAnalysisWizard : Window
    {
        public ProjectDetails ProjectDetails { get; private set; }

        public GatherAnalysisWizard()
        {
            InitializeComponent();
            ProjectDetails = new ProjectDetails();
            this.DataContext = ProjectDetails;
        }
        
        private void WizardCancelled(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WizardFinished(object sender, RoutedEventArgs e)
        {
            var data = ProjectDetails;
            Close();
        }
    }
}
