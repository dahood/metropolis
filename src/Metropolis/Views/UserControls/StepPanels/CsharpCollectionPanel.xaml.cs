using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using Metropolis.Common.Extensions;
using Metropolis.TipOfTheDay;
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
        
        private void NavigateToSite(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            if (link == null) return;

            Process.Start(link.NavigateUri.ToString());
        }
        
        private static string GetFileName(string initialFile = null)
        {
            var dialog = new OpenFileDialog {FileName = initialFile, Filter = @"Solution Files (*.sln)|*.sln;" };
            dialog.ShowDialog();
            return dialog.FileName != string.Empty ? dialog.FileName : null;
        }

        private void ShowCSharpToolTip(object sender, RoutedEventArgs e)
        {
            TipOfTheDay.Show<CSharpTipOfTheDay>();
        }

        private void FindSolutionFile(object sender, RoutedEventArgs e)
        {
            var solutionFile = GetFileName(ProjectDetails.ProjectFile);
            if (solutionFile.IsEmpty()) return;
            ProjectDetails.ProjectFile = solutionFile;
        }

        private void BuildSolution(object sender, RoutedEventArgs e)
        {

            IgnoreTabItem.IsEnabled = true;
            IgnoreTabItem.IsSelected = true;
        }
    }
}