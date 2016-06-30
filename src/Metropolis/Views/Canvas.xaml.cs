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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using Metropolis.Api.Domain;
using Metropolis.Api.Extensions;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Camera;
using Metropolis.Common.Models;
using Metropolis.Layout;
using Metropolis.Models;
using Metropolis.TipOfTheDay;
using Metropolis.ViewModels;

namespace Metropolis.Views
{
    public partial class Canvas : ISceneProvider, IDisplayInstanceInformation
    {
        private readonly InstanceInformationFacade highlightedInstance;
        private readonly MouseMovement mouseMovement;

        public Canvas()
        {
            InitializeComponent();
            highlightedInstance = new InstanceInformationFacade(this);
            mouseMovement = new MouseMovement(this);

            Initialize3DModel();
            HookupEventHandlers();
            
            DataContext = App.ViewModel;
            LoadDefaultProject();
        }

        public CodeBase CodeBase => App.CodeBase;
        private static IWorkspaceProvider WorkSpaceProvider => App.WorkspaceProvider;

        public AbstractLayout Layout { get; private set; } = new SquaredLayout();
        public AbstractHeatMap HeatMap { get; private set; } = new ToxicityHeatMap();

        public RepositorySourceType SourceType => CodeBase.SourceType;

        public void ShowCodeInspector()
        {
            var viewModel = new CodeInspectorViewModel
            {
                SourceType = CodeBase.SourceType,
                FileContents = WorkSpaceProvider.GetFileContents(highlightedInstance.GetPhysicalFilePath()),
                Instance = highlightedInstance.Instance
            };
            CodeInspector.ShowContent(viewModel);
        }

        public Viewport3D ViewPort => viewPort;
        public Model3DGroup Model { get; } = new Model3DGroup();

        public PerspectiveCamera GetCamera()
        {
            return (PerspectiveCamera) viewPort.Camera;
        }

        private void Initialize3DModel()
        {
            var modelVisual3D = new ModelVisual3D {Content = Model};
            viewPort.Children.Add(modelVisual3D);
        }

        private void LoadDefaultProject()
        {
            WorkSpaceProvider.LoadDefault();
            DisplayWorkspaceDetails();
        }

        private void HookupEventHandlers()
        {
            ListenForLayoutChanges();
            ListenForHeatMapChanges();
        }

        private void ListenForLayoutChanges()
        {
            SquareLayoutToggleButton.Checked += (sender, e) => { ChangeLayout(new SquaredLayout(), HeatMap); };
            CityLayoutToggleButton.Checked += (sender1, e1) => { ChangeLayout(new CityLayout(), HeatMap); };
            GoldenRatioLayoutToggleButton.Checked += (sender2, e2) => { ChangeLayout(new GoldenRatioLayout(), HeatMap); };
        }

        private void ListenForHeatMapChanges()
        {
            ToxicityToggleButton.Checked += (sender, e) => { ChangeLayout(Layout, new ToxicityHeatMap()); };
            DuplicateToggleButton.Checked += (sender1, e1) => { ChangeLayout(Layout, new DuplicateHeatMap()); };
        }

        private void ResetCamera(object sender, RoutedEventArgs e)
        {
            mouseMovement.Reset();
        }

        private void Renderlayout()
        {
            highlightedInstance.ClearDisplay();
            using (new WaitCursor())
                Layout.ModelCity(Model, CodeBase, HeatMap);
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.Create();
            Renderlayout();
            DisplayWorkspaceDetails();
        }

        private void LoadProject(object sender, RoutedEventArgs e)
        {
            try
            {
                WorkSpaceProvider.Load();
            }
            catch (ApplicationException ae)
            {
                MessageBox.Show(ae.Message);
            }
            DisplayWorkspaceDetails();
        }

        private void DisplayWorkspaceDetails()
        {
            App.ViewModel.ProjectName = CodeBase.Name;
            SetInstanceLabel(CodeBase.SourceType);
            LocTextBlock.Text = CodeBase.LinesOfCode.ToString("N0", CultureInfo.InvariantCulture);
            TypesTextBlock.Text = CodeBase.NumberOfTypes.ToString("N0", CultureInfo.InvariantCulture);
            ToxicityTextBlock.Text = CodeBase.AverageToxicity().ToString("N2", CultureInfo.InvariantCulture);
            CodeDensityTextBlock.Text = CodeBase.Density().ToString("N2", CultureInfo.InvariantCulture);
            DuplicateTextBlock.Text = CodeBase.Duplicates().ToString("P", CultureInfo.InvariantCulture);
            Renderlayout();
        }

        private void SetInstanceLabel(RepositorySourceType repositorySourceType)
        {
            switch (repositorySourceType)
            {
                case RepositorySourceType.CSharp:
                    InstanceLabel.Content = "Types";
                    break;
                case RepositorySourceType.Java:
                    InstanceLabel.Content = "Types";
                    break;
                case RepositorySourceType.ECMA:
                    InstanceLabel.Content = "Files";
                    break;
                default:
                    InstanceLabel.Content = "Types";
                    break;
            }
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            //TODO: Clean this up
            CodeBase.Name = App.ViewModel.ProjectName;
            WorkSpaceProvider.Save();
        }

        private void RunCsvExport(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.RunCsvExport();
        }

        private void RenameProject(object sender, RoutedEventArgs e)
        {
            new ProjectProperties().Show();
        }

        private void LoadToxicity(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.LoadToxicity();
            DisplayWorkspaceDetails();
        }

        private void LoadVisualStudioMetrics(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.LoadVisualStudioMetrics();
            DisplayWorkspaceDetails();
        }

        private void LoadCheckStyles(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.LoadCheckStyles();
            DisplayWorkspaceDetails();
        }

        private void LoadEsLintCheckStyles(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.LoadEsLintCheckStyles();
            DisplayWorkspaceDetails();
        }

        private void LoadSourceLinesOfCode(object sender, RoutedEventArgs e)
        {
            var header = ((RibbonButton) e.Source).Label;
            var extension = new Regex("[()]").Split(header)[1];

            WorkSpaceProvider.LoadSourceLinesOfCode(extension.ToEnumByDescription<FileInclusion>());
            DisplayWorkspaceDetails();
        }

        private void Shutdown(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeLayout(AbstractLayout newLayout, AbstractHeatMap heatmap)
        {
            Layout = newLayout;
            HeatMap = heatmap;
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
                SearchSuggestions.ItemsSource = CodeBase.AllInstances.Where(
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
            WorkSpaceProvider.RunCSharpToxicity();
            DisplayWorkspaceDetails();
        }

        private void RunJavaAnalzer(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.RunJavaToxicity();
            DisplayWorkspaceDetails();
        }

        private void RunJavascriptAnalzer(object sender, RoutedEventArgs e)
        {
            WorkSpaceProvider.RunJavascriptToxicity();
            DisplayWorkspaceDetails();
        }

        private void StartMetroBot(object sender, RoutedEventArgs e)
        {
            App.ViewModel.IsFxCopInstalled = WorkSpaceProvider.IsFxcopMetricsInstalled;
            var metroBot = new MetroBot();
            metroBot.DisplayWorkspaceDetails += (sndr, args) => DisplayWorkspaceDetails();
            metroBot.Show();
        }

        private void ViewProgressLog(object sender, RoutedEventArgs e)
        {
            Spinner.Show();
            App.ShowLog();
        }

        private void ToggleLayout(object sender, RoutedEventArgs e)
        {
            var toggleButtons = new[] {SquareLayoutToggleButton, CityLayoutToggleButton, GoldenRatioLayoutToggleButton};

            var target = sender as RibbonToggleButton;
            if (target == null) return;
            target.IsChecked = true;

            toggleButtons.Where(x => !ReferenceEquals(x, sender)).ForEach(each => each.IsChecked = false);
        }

        private void ToggleHeatmap(object sender, RoutedEventArgs e)
        {
            var toggleButtons = new[] { ToxicityToggleButton, DuplicateToggleButton };

            var target = sender as RibbonToggleButton;
            if (target == null) return;
            target.IsChecked = true;

            toggleButtons.Where(x => !ReferenceEquals(x, sender)).ForEach(each => each.IsChecked = false);
        }

        private void LoadCanvas(object sender, RoutedEventArgs e)
        {
            SetWindowState();
            SetVersion();
            LoadTipOfTheDay();
        }

        private void LoadTipOfTheDay()
        {
            if (!WorkSpaceProvider.ShowTips) return;
            ShowTipOfTheDay(this, new RoutedEventArgs());
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

        private void SetWindowState()
        {
            const double reductionFactor = 0.85;
            var workArea = SystemParameters.WorkArea;
            Width = workArea.Width*reductionFactor;
            Height = workArea.Height*reductionFactor;
            Left = (workArea.Width - Width)/2;
            Top = (workArea.Height - Height)/2;
        }

        private void SetVersion()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            Title = $"Metropolis beta v{version.Minor}.{version.Build}.{version.Revision}";
        }

        private void ViewMetricsFolder(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", WorkSpaceProvider.MetricsOutputFolder);
        }


        private void ProjectWiki(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/dahood/metropolis/wiki/Home");
        }

        private void CameraGuide(object sender, RoutedEventArgs e)
        {
            TipOfTheDay.Show<CameraControlToolTip>();
        }

        private void NewVersion(object sender, RoutedEventArgs e)
        {
            Process.Start("https://www.npmjs.com/package/metropolis");
        }

        private void AboutMetropolis(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\u00A9 Copyright 2016 - present, \n" +
                            "Jonathan McCracken, Richard Hurst, and Greg Cook All rights reserved.\n" +
                            "Metropolis is licensed under BSD (see LICENSE file for details)", "About Metropolis", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void ShowTipOfTheDay(object sender, RoutedEventArgs e)
        {
            new TipOfTheDay().ShowDialog();
        }

        private void UserPreferences(object sender, RoutedEventArgs e)
        {
            new UserPreferences().Show();
        }
    }
}