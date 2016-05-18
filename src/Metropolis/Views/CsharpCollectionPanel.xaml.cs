using System.Windows;
using System.Windows.Forms;
using Metropolis.Api.Extensions;
using Metropolis.ViewModels;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Metropolis.Views
{
    /// <summary>
    ///     Panel just for collection of CSharp metrics
    ///     - Must install Visual Studio & Metrics Powertools for VS for this to work
    /// </summary>
    public partial class CsharpCollectionPanel
    {
        public CsharpCollectionPanel()
        {
            InitializeComponent();
        }

        public ProjectDetailsViewModel ProjectDetails => (ProjectDetailsViewModel)DataContext;

        private void OnCSharpFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("C#", ProjectDetails.SourceDirectory);
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.SourceDirectory = sourceDirectory;
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
            return result == DialogResult.OK
                ? dialog.SelectedPath
                : string.Empty;
        }

        private void OnLocateIgnoreFile(object sender, RoutedEventArgs e)
        {
            var file = GetFileName();
            if (file == null) return;
            ProjectDetails.IgnoreFile = file;
        }
        private string GetFileName()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();
            if (dialog.FileName != string.Empty)
                return dialog.FileName;
            return null;
        }
    }
}