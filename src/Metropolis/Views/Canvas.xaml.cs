using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Services;
using Metropolis.Camera;
using Metropolis.Common.Models;
using Metropolis.Layout;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    public partial class Canvas : RibbonWindow, ISceneProvider, IDisplayInstanceInformation
    {
        private readonly RotationalMovement rotationalMovement;
        private readonly CameraMovement cameraMovement;
        private readonly InstanceInformationFacade highlightedInstance;
        private readonly IWorkspaceProvider workspaceProvider;
        private readonly ProgressLog progressLog;

        private AbstractLayout layout = new SquaredLayout();

        public Canvas()
        {
            InitializeComponent();
            rotationalMovement = new RotationalMovement(this);
            cameraMovement = new CameraMovement(this);
            highlightedInstance = new InstanceInformationFacade(this);
            workspaceProvider = new WorkspaceProvider(new CodebaseService(), new ProjectService(), new AnalysisServices());
            progressLog = new ProgressLog();

            SetSliders();

            InitializeModel();
            HookupEventHandlers();
            LoadDefaultProject();
        }

        private void InitializeModel()
        {
            var modelVisual3D = new ModelVisual3D { Content = Model };
            viewPort.Children.Add(modelVisual3D);
        }

        private void LoadDefaultProject()
        {
            workspaceProvider.LoadDefault();
            DisplayWorkspaceDetails();
        }

        private void HookupEventHandlers()
        {
            MouseWheel += HandleMouseWheel;
            ListenForLayoutChanges();
        }

        private void ListenForLayoutChanges()
        {
            Square.Checked += (sender, e) => { ChangeLayout(new SquaredLayout()); };
            City.Checked += (sender1, e1) => { ChangeLayout(new CityLayout()); };
            GoldenRatio.Checked += (sender2, e2) => { ChangeLayout(new GoldenRatioLayout()); };
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            positionZSlider.Value = positionZSlider.Value + (e.Delta > 0 ? 1 : -1);
            e.Handled = true;
        }

        public AbstractLayout Layout => layout;
        public Viewport3D ViewPort => viewPort;
        public Model3DGroup Model { get; } = new Model3DGroup();

        public RepositorySourceType SourceType => workspaceProvider.Workspace.SourceType;

        public PerspectiveCamera GetCamera()
        {
            return (PerspectiveCamera)viewPort.Camera;
        }

        public void SetClassInformation(string text)
        {
            codeInspectorText.Inlines.Clear();
            codeInspectorText.Inlines.Add(text);
        }

        private void ChangeCameraSettings(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            cameraMovement.Update(positionXSlider.Value, positionYSlider.Value, positionZSlider.Value,
                fieldOfViewZSlider.Value);
        }

        private void ResetCamera(object sender, RoutedEventArgs e)
        {
            SetSliders();
            rotationalMovement.Reset();
        }

        private void SetSliders()
        {
            positionXSlider.Value = 0;
            positionYSlider.Value = 0;
            positionZSlider.Value = 0;
            fieldOfViewZSlider.Value = 0;
        }

        private void Renderlayout()
        {
            highlightedInstance.ClearDisplay();
            using (new WaitCursor())
                layout.ModelCity(Model, workspaceProvider.Workspace);
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            workspaceProvider.Create();
            Renderlayout();
            DisplayWorkspaceDetails();
        }

        private void LoadProject(object sender, RoutedEventArgs e)
        {
            workspaceProvider.Load();
            DisplayWorkspaceDetails();
        }

        private void DisplayWorkspaceDetails()
        {
            ProjectNameTextBox.Text = workspaceProvider.Workspace.Name;
            LocTextBlock.Text = workspaceProvider.Workspace.LinesOfCode.ToString("N0", CultureInfo.InvariantCulture);
            TypesTextBlock.Text = workspaceProvider.Workspace.NumberOfTypes.ToString("N0", CultureInfo.InvariantCulture);
            ToxicityTextBlock.Text = workspaceProvider.Workspace.AverageToxicity().ToString("N4", CultureInfo.InvariantCulture);
            Renderlayout();
        }
        private void Project_Info_Changed(object sender, TextChangedEventArgs e)
        {
            workspaceProvider.Workspace.Name = ProjectNameTextBox.Text;
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            workspaceProvider.Save();
        }

        private void LoadToxicity(object sender, RoutedEventArgs e)
        {
            workspaceProvider.LoadToxicity();
            DisplayWorkspaceDetails();
        }

        private void LoadVisualStudioMetrics(object sender, RoutedEventArgs e)
        {
            workspaceProvider.LoadVisualStudioMetrics();
            DisplayWorkspaceDetails();
        }

        private void LoadCheckStyles(object sender, RoutedEventArgs e)
        {
            workspaceProvider.LoadCheckStyles();
            DisplayWorkspaceDetails();
        }
        private void LoadEsLintCheckStyles(object sender, RoutedEventArgs e)
        {
            workspaceProvider.LoadEsLintCheckStyles();
            DisplayWorkspaceDetails();
        }

        private void LoadSourceLinesOfCode(object sender, RoutedEventArgs e)
        {
            var header = ((RibbonButton) e.Source).Label;
            var extension = new Regex("[()]").Split((string)header)[1];

            workspaceProvider.LoadSourceLinesOfCode(extension.ToEnumByDescription<FileInclusion>());
            DisplayWorkspaceDetails();
        }

        private void Shutdown(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeLayout(AbstractLayout newLayout)
        {
            layout = newLayout;
            Renderlayout();
            ResetCamera(null, null);
        }

        private void searchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchSuggestions.ItemsSource = null;
            if (searchText.Text.Length > 2)
            {
                var searchQuery = searchText.Text;
                SearchSuggestions.DisplayMemberPath = "Name";
                SearchSuggestions.ItemsSource = workspaceProvider.Workspace.AllInstances.Where(
                        x => x.QualifiedName.IndexOf(searchQuery, StringComparison.CurrentCultureIgnoreCase) >= 0);
            }
            SearchSuggestions.Items.Refresh();
        }

        private void searchSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedClass = (Instance)SearchSuggestions.SelectedItem;
            if (selectedClass != null)
                highlightedInstance.DisplayClass(selectedClass);
        }

        private void clearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            searchText.Text = string.Empty;
        }

        private void RunCSharpAnalzer(object sender, RoutedEventArgs e)
        {
            workspaceProvider.RunCSharpToxicity();
            DisplayWorkspaceDetails();
        }

        private void RunJavaAnalzer(object sender, RoutedEventArgs e)
        {
            workspaceProvider.RunJavaToxicity();
            DisplayWorkspaceDetails();
        }

        private void RunJavascriptAnalzer(object sender, RoutedEventArgs e)
        {
            Spinner.Show();
            workspaceProvider.RunJavascriptToxicity();
            DisplayWorkspaceDetails();
            Spinner.Hide();
        }
        

        private void RunCsvExport(object sender, RoutedEventArgs e)
        {
            workspaceProvider.RunCsvExport();
        }

        private void StartMetroBot(object sender, RoutedEventArgs e)
        {
            var metroBot = new MetroBot();
            var result = metroBot.ShowDialog()??false;

            if (!result) return;
            Spinner.Show();
            using (new WaitCursor())
            {
                workspaceProvider.Analyze(BuildArguments(metroBot.ProjectDetails));
                DisplayWorkspaceDetails();
            }
            Spinner.Hide();
            
        }

        private void ViewProgressLog(object sender, RoutedEventArgs e)
        {
            Spinner.Show();
            progressLog.Visibility = Visibility.Visible;
        }

        private static MetricsCommandArguments BuildArguments(ProjectDetailsViewModel projectDetails)
        {
            return new MetricsCommandArguments
            {
                ProjectName =projectDetails.ProjectName,
                RepositorySourceType = projectDetails.RepositorySourceType,
                MetricsOutputDirectory = projectDetails.MetricsOutputDirectory,
                IgnoreFile = projectDetails.IgnoreFile,
                SourceDirectory = projectDetails.SourceDirectory
            };
        }

        private void ProjectWiki(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/dahood/metropolis/wiki/Home");
        }

        private void NewVersion(object sender, RoutedEventArgs e)
        {
            //TODO: Potentially allow to check npm installed version versus what is on NPMJS.com
            Process.Start("https://www.npmjs.com/package/metropolis");
        }

        private void AboutMetropolis(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\u00A9 Copyright 2016 - present, \n" +
                            "Jonathan McCracken, Richard Hurst, and Greg Cook All rights reserved.\n" +
                            "Metropolis is licensed under BSD (see LICENSE file for details)", "About Metropolis", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}