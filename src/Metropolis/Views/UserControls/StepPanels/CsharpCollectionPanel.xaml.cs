

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Metropolis.Camera;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.Controllers;
using Metropolis.TipOfTheDay;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls.StepPanels
{
    /// <summary>
    ///     Panel just for collection of CSharp metrics
    ///     - Must install Visual Studio & Metrics Powertools for VS for this to work
    /// </summary>
    public partial class CsharpCollectionPanel : ICSharpCollectionView
    {
        private CsharpCollectionController controller;

        public event EventHandler BuildRequested;
        public event EventHandler<SolutionFileArgs> SolutionFileSelected;
        public event EventHandler<CreateIgnoreFileArgs> RunAnalysisRequest;

        public ProjectDetailsViewModel ProjectDetails => (ProjectDetailsViewModel) DataContext;
        
        public CsharpCollectionPanel()
        {
            InitializeComponent();
            controller = new CsharpCollectionController(this, App.WorkspaceProvider);
        }

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
            using (new WaitCursor())
            {
                BuildRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private void RunAnalysis(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                var ignoreFileList = IgnoreFileDataGrid.ItemsSource.OfType<FileDto>().Where(x => x.Ignore).ToList();
                RunAnalysisRequest?.Invoke(this, new CreateIgnoreFileArgs {IngoreFiles = ignoreFileList});
                
            }
        }

        private void FindSolutionFolder(object sender, RoutedEventArgs e)
        {
        }

        private void SolutionFileChanged(object sender, TextChangedEventArgs e)
        {
            SolutionFileSelected?.Invoke(this, new SolutionFileArgs {SolutionFile = SolutionFileTextBox.Text});
        }

        public void ShowBuildArtifacts(IEnumerable<FileDto> artifacts)
        {
            IgnoreTabItem.IsEnabled = true;
            IgnoreTabItem.IsSelected = true;
            IgnoreFileDataGrid.ItemsSource = artifacts;
        }
    }

    public class SolutionFileArgs : EventArgs
    {
        public string SolutionFile { get; set; }
    }

    public class CreateIgnoreFileArgs : EventArgs
    {
        public IEnumerable<FileDto> IngoreFiles { get; set; }
    }
}