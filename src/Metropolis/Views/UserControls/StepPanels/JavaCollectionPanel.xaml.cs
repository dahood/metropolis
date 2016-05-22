using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using Metropolis.Common.Extensions;
using Metropolis.ViewModels;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Metropolis.Views.UserControls.StepPanels
{
    /// <summary>
    ///     Panel just for collection of Java metrics
    ///     - Must install Java for this to work
    /// </summary>
    public partial class JavaCollectionPanel
    {
        public JavaCollectionPanel()
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

        private void NavigateToSite(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            if (link == null) return;

            System.Diagnostics.Process.Start(link.NavigateUri.ToString());
        }

        private void HideShowInfoBox(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");
            Info.Visibility = key != null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}