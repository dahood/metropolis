using System;
using System.Windows;
using System.Windows.Controls;
using Metropolis.Camera;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    /// <summary>
    ///     Metrobot is amazing... it can collect all the metrics you need from all the tools in the world....
    ///     Currently the world is only Checkstyle, Eslint, and Sloc...but one day Metrobot will collect metrics for the entire
    ///     world
    /// </summary>
    public partial class MetroBot
    {
        public MetroBot()
        {
            InitializeComponent();
            ProjectDetails = App.ViewModel;
            DataContext = ProjectDetails;
        }

        public event EventHandler DisplayWorkspaceDetails;

        public ProjectDetailsViewModel ProjectDetails { get; }
        private static IWorkspaceProvider WorkSpaceProvider => App.WorkspaceProvider;

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnProceed(object sender, RoutedEventArgs e)
        {
            Spinner.Show();
            using (new WaitCursor())
            {
                WorkSpaceProvider.Analyze(ProjectDetails);
                DisplayWorkspaceDetails?.Invoke(this, new EventArgs());
            }
            Spinner.Hide();
            Close();
        }

        private void SourceCodeSelected(object sender, SelectionChangedEventArgs e)
        {
            var selection = (RepositorySourceType) Enum.Parse(typeof(RepositorySourceType), SourceTypeCombobox.SelectedItem.ToString());
            switch (selection)
            {
                case RepositorySourceType.CSharp:
                    CSharpPanel.Visibility = Visibility.Visible;
                    JavaPanel.Visibility = Visibility.Collapsed;
                    EcmaScriptPanel.Visibility = Visibility.Collapsed;
                    break;
                case RepositorySourceType.Java:
                    CSharpPanel.Visibility = Visibility.Collapsed;
                    JavaPanel.Visibility = Visibility.Visible;
                    EcmaScriptPanel.Visibility = Visibility.Collapsed;
                    break;
                case RepositorySourceType.ECMA:
                    CSharpPanel.Visibility = Visibility.Collapsed;
                    JavaPanel.Visibility = Visibility.Collapsed;
                    EcmaScriptPanel.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ApplicationException("Unsupported RepositorySourceType: " + sender);
            }
        }
    }
}