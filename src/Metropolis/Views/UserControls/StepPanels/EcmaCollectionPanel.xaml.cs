using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.TipOfTheDay;
using Metropolis.ViewModels;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Metropolis.Views.UserControls.StepPanels
{
    /// <summary>
    ///     Panel just for collection of ECMA/Javascript metrics
    /// </summary>
    public partial class EcmaCollectionPanel
    {
        public EcmaCollectionPanel()
        {
            InitializeComponent();
            LanguageDialectComboBox.ItemsSource = Enum.GetValues(typeof(EslintPasringOptions)).Cast<EslintPasringOptions>();
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

        private void SetDialect(object sender, SelectionChangedEventArgs e)
        {
            ProjectDetails.EcmaScriptDialect =
                (EslintPasringOptions) Enum.Parse(typeof(EslintPasringOptions), LanguageDialectComboBox.SelectedItem.ToString());
        }

        private void ShowECMAHelp(object sender, RoutedEventArgs e)
        {
            TipOfTheDay.Show<EcmaScriptTipOfTheDay>();
        }
    }
}