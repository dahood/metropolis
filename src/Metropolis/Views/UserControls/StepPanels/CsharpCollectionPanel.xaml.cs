using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using Metropolis.Common.Extensions;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls.StepPanels
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

        public ProjectDetailsViewModel ProjectDetails => (ProjectDetailsViewModel) DataContext;

        private void OnCSharpFindDirectory(object sender, RoutedEventArgs e)
        {
            var sourceDirectory = GetSourceDirectory("C#", ProjectDetails.SourceDirectory);
            if (sourceDirectory.IsNotEmpty())
                ProjectDetails.SourceDirectory = sourceDirectory;
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

            Process.Start(link.NavigateUri.ToString());
        }

        private void OnLocateIgnoreFile(object sender, RoutedEventArgs e)
        {
            var file = GetFileName(ProjectDetails.IgnoreFile);
            if (file == null) return;
            ProjectDetails.IgnoreFile = file;
        }

        private static string GetFileName(string initialFile = null)
        {
            var dialog = new OpenFileDialog {FileName = initialFile};
            dialog.ShowDialog();
            return dialog.FileName != string.Empty ? dialog.FileName : null;
        }

        private void HideShowInfoBox(object sender, DependencyPropertyChangedEventArgs e)
        {
            //TODO: Scan all possible directories? e.g. DriveInfo.GetDrives().Where(x => x.IsReady == true)

            var vsInstallDirectory = Directory.Exists(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE");
            var metricsInstallDirectory =
                Directory.Exists(@"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Team Tools\Static Analysis Tools\FxCop");

            if (vsInstallDirectory) InfoVisualStudio.Visibility = Visibility.Collapsed;
            if (metricsInstallDirectory) InfoPowerTools.Visibility = Visibility.Collapsed;
        }
    }
}