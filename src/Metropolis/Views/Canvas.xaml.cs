using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Api.Extensions;
using Metropolis.Api.Microservices;
using Metropolis.Camera;
using Metropolis.Common;
using Metropolis.Common.Models;
using Metropolis.Layout;

namespace Metropolis.Views
{
    public partial class Canvas : ISceneProvider, IDisplayClassInformation
    {
        private readonly RotationalMovement rotationalMovement;
        private readonly CameraMovement cameraMovement;
        private readonly ClassInformationFacade highlightedClass;
        private readonly IWorkspaceProvider workspaceProvider;

        private AbstractLayout layout = new SquaredLayout();

        public Canvas()
        {
            InitializeComponent();
            rotationalMovement = new RotationalMovement(this);
            cameraMovement = new CameraMovement(this);
            highlightedClass = new ClassInformationFacade(this);
            workspaceProvider = new WorkspaceProvider(new CodebaseService(), new ProjectService());

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
            highlightedClass.ClearDisplay();
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
            var header = ((MenuItem) e.Source).Header;
            var extension = new Regex("[()]").Split((string)header)[1];

            workspaceProvider.LoadSourceLinesOfCode(extension.ToEnumByDescription<FileInclusion>());
            DisplayWorkspaceDetails();
        }

        private void NewVersion(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/dahood/metropolis");
        }

        private void AboutMetropolis(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\u00A9 Copyright 2016 - present, \n" +
                "Jonathan McCracken, Richard Hurst, and Greg Cook All rights reserved.\n" +
                "Metropolis is licensed under BSD (see LICENSE file for details)", "About Metropolis", MessageBoxButton.OK, MessageBoxImage.Information);
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
            searchSuggestions.ItemsSource = null;
            if (searchText.Text.Length > 2)
            {
                var searchQuery = searchText.Text;
                searchSuggestions.DisplayMemberPath = "Name";
                searchSuggestions.ItemsSource = workspaceProvider.Workspace.AllClasses.Where(
                        x => x.QualifiedName.IndexOf(searchQuery, StringComparison.CurrentCultureIgnoreCase) >= 0);
            }
            searchSuggestions.Items.Refresh();
        }

        private void searchSuggestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedClass = (Class)searchSuggestions.SelectedItem;
            if (selectedClass != null)
                highlightedClass.DisplayClass(selectedClass);
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
            workspaceProvider.RunJavascriptToxicity();
            DisplayWorkspaceDetails();
        }

        private void RunCsvExport(object sender, RoutedEventArgs e)
        {
            workspaceProvider.RunCsvExport();
        }

        private void RunWizard(object sender, RoutedEventArgs e)
        {
            var wizard = new GatherAnalysisWizard();
            try
            {
                wizard.ShowDialog();
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message, exception);
            }
        }
    }
}