using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Camera;
using Metropolis.Common.Models;
using Metropolis.Layout;

namespace Metropolis.Views
{
    public partial class Canvas : ISceneProvider, IDisplayInstanceInformation
    {
        private readonly CameraMovement cameraMovement;
        private readonly InstanceInformationFacade highlightedInstance;
        private readonly ProgressLog progressLog;
        private readonly RotationalMovement rotationalMovement;
        private readonly IWorkspaceProvider workspaceProvider;

        public Canvas()
        {
            InitializeComponent();
            rotationalMovement = new RotationalMovement(this);
            cameraMovement = new CameraMovement(this);
            highlightedInstance = new InstanceInformationFacade(this);
            workspaceProvider = new WorkspaceProvider();
            progressLog = new ProgressLog();

            SetSliders();

            InitializeModel();
            HookupEventHandlers();
            LoadDefaultProject();
        }

        public AbstractLayout Layout { get; private set; } = new SquaredLayout();

        public RepositorySourceType SourceType => workspaceProvider.Workspace.SourceType;

        public void SetClassInformation(string text)
        {
            codeInspectorText.Inlines.Clear();
            codeInspectorText.Inlines.Add(text);
        }

        public Viewport3D ViewPort => viewPort;
        public Model3DGroup Model { get; } = new Model3DGroup();

        public PerspectiveCamera GetCamera()
        {
            return (PerspectiveCamera) viewPort.Camera;
        }

        private void InitializeModel()
        {
            var modelVisual3D = new ModelVisual3D {Content = Model};
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
            SquareLayoutToggleButton.Checked += (sender, e) => { ChangeLayout(new SquaredLayout()); };
            CityLayoutToggleButton.Checked += (sender1, e1) => { ChangeLayout(new CityLayout()); };
            GoldenRatioLayoutToggleButton.Checked += (sender2, e2) => { ChangeLayout(new GoldenRatioLayout()); };
        }

        private void HandleMouseWheel(object sender, MouseWheelEventArgs e)
        {
            positionZSlider.Value = positionZSlider.Value + (e.Delta > 0 ? 1 : -1);
            e.Handled = true;
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
                Layout.ModelCity(Model, workspaceProvider.Workspace);
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
            var extension = new Regex("[()]").Split(header)[1];

            workspaceProvider.LoadSourceLinesOfCode(extension.ToEnumByDescription<FileInclusion>());
            DisplayWorkspaceDetails();
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeLayout(AbstractLayout newLayout)
        {
            Layout = newLayout;
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
            var selectedClass = (Instance) SearchSuggestions.SelectedItem;
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
            //App.ShowLog();
            //Spinner.Show();
            workspaceProvider.RunJavascriptToxicity();
            DisplayWorkspaceDetails();
            //Spinner.Hide();
            //App.ShowLog();
        }


        private void RunCsvExport(object sender, RoutedEventArgs e)
        {
            workspaceProvider.RunCsvExport();
        }

        private void StartMetroBot(object sender, RoutedEventArgs e)
        {
            var metroBot = new MetroBot();
            var result = metroBot.ShowDialog() ?? false;

            if (!result) return;
            Spinner.Show();
            using (new WaitCursor())
            {
                workspaceProvider.Analyze(metroBot.ProjectDetails);
                DisplayWorkspaceDetails();
            }
            Spinner.Hide();
        }

        private void ViewProgressLog(object sender, RoutedEventArgs e)
        {
            Spinner.Show();
            progressLog.Visibility = Visibility.Visible;
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

        private void TakeScreenshot(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                var screenshotFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "metro-screenshot" + DateTime.Now.Ticks + ".png");
                ScaleScreenshot(screenshotFileName, false);
                Process.Start(screenshotFileName);
            }
        }

        private void TakeScreenshotWithCaption(object sender, RoutedEventArgs e)
        {
            using (new WaitCursor())
            {
                var screenshotFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "metro-screenshot" + DateTime.Now.Ticks + ".png");
                ScaleScreenshot(screenshotFileName, true);
                Process.Start(screenshotFileName);
            }
        }

        private void ScaleScreenshot(string screenshotFileName, bool withCaption)
        {
            const double wpfDpi = 96; // screen only image quality
            const double targetDpi = 600; // print quality images
            const double scale = targetDpi/wpfDpi;

            var bitmap = new RenderTargetBitmap(
                (int) (scale*(viewPort.ActualWidth + 1)),
                (int) (scale*(viewPort.ActualHeight + 1)),
                scale*96,
                scale*96, PixelFormats.Pbgra32);

            bitmap.Render(viewPort);
            if (withCaption)
                AddCaptionToScreenShot(bitmap);
            SaveToPng(screenshotFileName, bitmap);
        }

        private void AddCaptionToScreenShot(RenderTargetBitmap bitmap)
        {
            CodeMetricsGrid.BorderBrush = Brushes.Black;
            bitmap.Render(CodeMetricsGrid);
            CodeMetricsGrid.BorderBrush = Brushes.White;
        }

        private static void SaveToPng(string screenshotFileName, RenderTargetBitmap renderTargetBitmap)
        {
            var pngImage = new PngBitmapEncoder {Interlace = PngInterlaceOption.Off};
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using (Stream fileStream = File.Create(screenshotFileName))
            {
                pngImage.Save(fileStream);
            }
        }

        private void ToggleLayout(object sender, RoutedEventArgs e)
        {
            var toggleButons = new[] {SquareLayoutToggleButton, CityLayoutToggleButton, GoldenRatioLayoutToggleButton};

            var target = sender as RibbonToggleButton;
            if (target == null) return;
            target.IsChecked = true;

            toggleButons.Where(x => x != sender).ForEach(each => each.IsChecked = false);
        }

        private void LoadCanvas(object sender, RoutedEventArgs e)
        {
            SetWindowState();
            SetVersion();
        }

        private void SetWindowState()
        {
            const double reductionFactor = 0.85;
            var workArea = SystemParameters.WorkArea;
            Width = workArea.Width*reductionFactor;
            Height = workArea.Height*reductionFactor;
            Top = (workArea.Width - Width)/2;
            Left = (workArea.Height - Height)/2;
        }

        private void SetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Title = $"Metropolis beta v{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void ViewMetricsFolder(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", workspaceProvider.MetricsOutputFolder);
        }
    }
}