using System.Windows;
using System.Windows.Forms;
using Metropolis.Api.Extensions;
using Metropolis.ViewModels;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Metropolis.Views
{
    /// <summary>
    /// Interaction logic for GatherAnalysisWizard.xaml
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
      
        private void OnCSharpFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("C#", ProjectDetails.SourceDirectory);
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.SourceDirectory= sourceDirectory;
        }

        private void OnMetricsOutputDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("Metrics Output Folder", ProjectDetails.MetricsOutputDirectory);
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.MetricsOutputDirectory = sourceDirectory;
        }

        private static string GetSourceDirectory(string type, string initialDirectory)
        {
            var dialog = new FolderBrowserDialog {Description = $"Locate {type} Source Directory", SelectedPath = initialDirectory};
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

        private void OnLocateIgnoreFile(object sender, RoutedEventArgs e)
        {
            var file = GetFileName("");
            if (file == null) return;
            ProjectDetails.IgnoreFile = file;
        }
        private static string GetFileName(string filter)
        {
            var dialog = new OpenFileDialog { Filter = filter };
            return dialog.ShowDialog().GetValueOrDefault(false) ? dialog.FileName : null;
        }
    }
}
