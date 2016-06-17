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

        private void HideShowInfoBox(object sender, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\JavaSoft\\Java Runtime Environment");
            Info.Visibility = key != null ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}