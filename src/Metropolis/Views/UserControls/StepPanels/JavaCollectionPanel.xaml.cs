using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using Metropolis.Utilities;
using Metropolis.ViewModels;
using Microsoft.Win32;

namespace Metropolis.Views.UserControls.StepPanels
{
    /// <summary>
    ///     Panel just for collection of Java metrics
    ///     - Must install Java for this to work
    /// </summary>
    public partial class JavaCollectionPanel : ICollectionView
    {
        public JavaCollectionPanel()
        {
            InitializeComponent();
        }

        public ProjectDetailsViewModel ProjectDetails => (ProjectDetailsViewModel) DataContext;
        public void RunAnalysis()
        {
            App.WorkspaceProvider.Analyze(ProjectDetails);
        }

        private void OnJavaFindDirectory(object sender, RoutedEventArgs e)
        {
            ProjectDetails.SourceDirectory = DialogUtils.GetSourceDirectory("Java", ProjectDetails.SourceDirectory);
        }

        private void NavigateToSite(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            if (link == null) return;

            Process.Start(link.NavigateUri.ToString());
        }

        private void HideShowInfoBox(object sender, RoutedEventArgs routedEventArgs)
        {
            var info = new ProcessStartInfo("cmd.exe", "/c \"" + "java -version " + "\"")
            {
                ErrorDialog = false,
                CreateNoWindow = true,
                UseShellExecute = false,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var process = Process.Start(info);
            if (process == null) return;
            process.WaitForExit();

            Info.Visibility = process.ExitCode == 0 ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}