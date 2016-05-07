using System.Windows;

namespace Metropolis.Views
{
    /// <summary>
    /// Interaction logic for GatherAnalysisWizard.xaml
    /// </summary>
    public partial class GatherAnalysisWizard : Window
    {
        public GatherAnalysisWizard()
        {
            InitializeComponent();
        }
        
        private void WizardCancelled(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WizardFinished(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
