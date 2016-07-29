using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Metropolis.Api.Extensions;
using Metropolis.Common.Models;
using Metropolis.TipOfTheDay;
using Metropolis.Utilities;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls.StepPanels
{
    /// <summary>
    ///     Panel just for collection of ECMA/Javascript metrics
    /// </summary>
    public partial class EcmaCollectionPanel : ICollectionView
    {
        public EcmaCollectionPanel()
        {
            InitializeComponent();
            LanguageDialectComboBox.ItemsSource = Enum.GetValues(typeof(EslintPasringOptions)).Cast<EslintPasringOptions>();
        }

        public ProjectDetailsViewModel ProjectDetails => (ProjectDetailsViewModel) DataContext;

        public void RunAnalysis()
        {
            App.WorkspaceProvider.Analyze(ProjectDetails);
        }

        private void OnCSharpFindDirectory(object sender, RoutedEventArgs e)
        {
            ProjectDetails.SourceDirectory = DialogUtils.GetSourceDirectory("JavaScript", ProjectDetails.SourceDirectory);
        }

        private void OnLocateIgnoreFile(object sender, RoutedEventArgs e)
        {
            ProjectDetails.IgnoreFile = DialogUtils.GetFileName(@"Eslint Ignore Files (.eslintignore)|*.eslintignore;", ProjectDetails.IgnoreFile);
        }

        private void SetDialect(object sender, SelectionChangedEventArgs e)
        {
            ProjectDetails.EcmaScriptDialect = LanguageDialectComboBox.SelectedItem.ToString().ToEnumExact<EslintPasringOptions>();
        }

        private void ShowECMAHelp(object sender, RoutedEventArgs e)
        {
            TipOfTheDay.Show<EcmaScriptTipOfTheDay>();
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

        private void NavigateToSite(object sender, RoutedEventArgs e)
        {
            var link = sender as Hyperlink;
            if (link == null) return;

            Process.Start(link.NavigateUri.ToString());
        }
    }
}