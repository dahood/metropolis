using System.Windows;
using System.Windows.Forms;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;

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
            DataContext = ProjectDetails;
        }
        
        private void WizardCancelled(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void WizardFinished(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            var data = ProjectDetails;
            Close();
        }

        private void OnCSharpFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("C#");
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.CSharpSourceDirectory= sourceDirectory;
        }

        private void OnJavaFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("Java");
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.JavaSourceDirectory = sourceDirectory;
        }
        private void OnEcmaFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("Ecma6/Javascript");
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.EcmaSourceDirectory = sourceDirectory;
        }

        private static string GetSourceDirectory(string type)
        {
            var dialog = new FolderBrowserDialog {Description = $"Locate {type} Source Directory"};
            var result = dialog.ShowDialog();
            return result == System.Windows.Forms.DialogResult.OK
                ? dialog.SelectedPath
                : string.Empty;
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
