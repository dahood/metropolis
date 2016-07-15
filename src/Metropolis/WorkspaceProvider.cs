using System;
using System.IO;
using System.Linq;
using CsvHelper;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.IO;
using Metropolis.Api.IO.AutoSave;
using Metropolis.Api.Readers;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Services;
using Metropolis.Camera;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using Microsoft.Win32;
using NLog;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IAnalysisService analysisService;
        private readonly ICodebaseService codebaseService;
        private readonly IUserPreferences userPreferences;
        private readonly IAutoSaveService autoSaveService;
        private readonly IFileSystem fileSystem;


        public WorkspaceProvider() : this(new CodebaseService(), new AnalysisServices(), new UserPreferences(), new FileSystem(), new AutoSaveService())
        {
        }

        public WorkspaceProvider(ICodebaseService codebaseService, IAnalysisService analysisService, IUserPreferences userPreferences, IFileSystem fileSystem,
                                 IAutoSaveService autoSaveService)
        {
            this.codebaseService = codebaseService;
            this.analysisService = analysisService;
            this.userPreferences = userPreferences;
            this.fileSystem = fileSystem;
            this.autoSaveService = autoSaveService;
            fileSystem.CreateMetropolisSpecialFolders();
            CodeBase = CodeBase.Empty();
        }

        public CodeBase CodeBase { get; private set; }
        public string MetricsOutputFolder => fileSystem.MetricsOutputFolder;
        public string ProjectBuildFolder => codebaseService.ProjectBuildFolder;
        public bool IsFxcopMetricsInstalled => File.Exists(analysisService.FxCopMetricsPath);

        public void Create()
        {
            CodeBase = CodeBase.Empty();
        }

        public void Save()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Metropolis Project File (*.project)|*.project",
                AddExtension = true
            };
            if (!dialog.ShowDialog().GetValueOrDefault(false)) return;

            using (new WaitCursor())
            {
                codebaseService.Save(CodeBase, dialog.FileName);
            }
        }

        public void Load()
        {
            OpenFile(Load, "Metropolis Project|*.project");
        }

        public void Load(string fileName)
        {
            CodeBase = codebaseService.Load(fileName);
        }
        
        public void LoadCheckStyles()
        {
            OpenFile(fileName =>
            {
                CodeBase.SourceType = RepositorySourceType.Java;
                var parser = CheckStylesReader.PuppyCrawlReader;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }

        public void LoadEsLintCheckStyles()
        {
            OpenFile(fileName =>
            {
                CodeBase.SourceType = RepositorySourceType.ECMA;
                var parser = CheckStylesReader.EslintReader;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }

        public void LoadSourceLinesOfCode(FileInclusion inclusion)
        {
            OpenFile(fileName =>
            {
                var parser = new SlocReader(inclusion);
                Parse(parser, fileName);
            }, "Source LOC |*.csv");
        }

        public void Analyze(ProjectDetailsViewModel viewModel)
        {
            CodeBase.SourceType = viewModel.RepositorySourceType;
            CodeBase.Name = viewModel.ProjectName;
            CodeBase = analysisService.Analyze(BuildArguments(viewModel));
        }

        public bool ShowTips
        {
            get { return userPreferences.ShowTipOfTheDay; }
            set { userPreferences.ShowTipOfTheDay = value; }
        }

        public string ScreenShotFolder => fileSystem.ScreenShotFolder;

        public ProjectBuildResult BuildSolution(ProjectBuildArguments args)
        {
            return codebaseService.BuildSolution(args);
        }

        public string DeriveProjectFolder(string text, string defaultValue=null)
        {
            return text.IsEmpty() ? defaultValue : Path.GetDirectoryName(text);
        }

        public string DeriveProjectName(string text, string defaultValue=null)
        {
            return text.IsEmpty() ? defaultValue : Path.GetFileNameWithoutExtension(text);
        }

        public void CreateIgnoreFile(ProjectDetailsViewModel projectDetails)
        {
            codebaseService.WriteIgnoreFile(projectDetails.ProjectName, projectDetails.ProjectFolder, projectDetails.FilesToIgnore);
        }

        public void SetUpDotNetBuild(ProjectDetailsViewModel projectDetails, string solutionFile)
        {
            projectDetails.ProjectName =  solutionFile.IsNotEmpty()? Path.GetFileNameWithoutExtension(solutionFile) : projectDetails.ProjectName;
            projectDetails.ProjectFolder =  solutionFile.IsNotEmpty()? Path.GetDirectoryName(solutionFile) : projectDetails.ProjectName;

            var buildPaths = codebaseService.GetBuildPaths(projectDetails.ProjectName);
            projectDetails.IgnoreFile = buildPaths.IgnoreFile;
            projectDetails.BuildOutputDirectory = buildPaths.BuildOutputDirectory;
            projectDetails.SourceDirectory = projectDetails.ProjectFolder;

            projectDetails.FilesToIgnore = codebaseService.GetIgnoreFilesForProject(projectDetails.ProjectFolder);
        }

        public FileContentsResult GetFileContents(string physicalFilePath)
        {
            return codebaseService.GetFileContents(physicalFilePath);
        }

        public void AutoSaveProject(ProjectDetailsViewModel projectDetails)
        {
            Apply(CodeBase, projectDetails);            
            autoSaveService.Save(CodeBase);
        }

        public void EnsureFolderExists(string path)
        {
            fileSystem.EnsureDirectoriesExist(path);
        }

        public bool AutoloadLastProject()
        {
            foreach (var each in fileSystem.GetAutloadProjects().Take(5))
            {
                if (AttemptProjectLoad(each)) return true;
            }
            CodeBase = codebaseService.LoadDefault();
            return true;
        }

        private bool AttemptProjectLoad(FileInfo projectFile)
        {
            try
            {
                if (Logger.IsDebugEnabled)
                    Logger.Debug($"attempting to load autosave project {projectFile}");
                CodeBase = codebaseService.Load(projectFile.FullName);
                return true;
            }
            catch (Exception e)
            {
                if (Logger.IsErrorEnabled)
                Logger.Error(e, $"attempting to load autosave project {projectFile}");
            }
            return false;
        }

        private static void Apply(CodeBase codeBase, ProjectDetailsViewModel projectDetails)
        {
            codeBase.ProjectFile = projectDetails.ProjectFile;
            codeBase.ProjectFolder = projectDetails.ProjectFolder;
            codeBase.IgnoreFile = projectDetails.IgnoreFile;
            codeBase.SourceBaseDirectory = projectDetails.SourceDirectory;
            codeBase.SourceType = projectDetails.RepositorySourceType;
        }

        public static MetricsCommandArguments BuildArguments(ProjectDetailsViewModel projectDetails)
        {
            return new MetricsCommandArguments
            {
                ProjectName = projectDetails.ProjectName,
                ProjectFolder = projectDetails.ProjectFolder,
                ProjectFile = projectDetails.ProjectFile,
                BuildOutputFolder = projectDetails.BuildOutputDirectory,
                RepositorySourceType = projectDetails.RepositorySourceType,
                IgnoreFile = projectDetails.IgnoreFile,
                SourceDirectory = projectDetails.SourceDirectory,
                EcmaScriptDialect =  projectDetails.EcmaScriptDialect
            };
        }

        public void RunJavaToxicity()
        {
            CodeBase.SourceType = RepositorySourceType.Java;
            CodeBase = new JavaToxicityAnalyzer().Analyze(CodeBase.AllInstances);
        }

        public void RunJavascriptToxicity()
        {
            CodeBase.SourceType = RepositorySourceType.ECMA;
            CodeBase = new JavascriptToxicityAnalyzer().Analyze(CodeBase.AllInstances);
        }

        public void RunCsvExport()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Metropolis CSV export (*.csv)|*.csv",
                AddExtension = true
            };
            if (!dialog.ShowDialog().GetValueOrDefault(false)) return;

            using (new WaitCursor())
            {
                using (var stream = new StreamWriter(dialog.FileName))
                {
                    var writer = new CsvWriter(stream);
                    writer.WriteHeader<Instance>();
                    CodeBase.AllInstances.ForEach(x => writer.WriteRecord(x));
                }
            }
        }

        private void Parse(IInstanceReader parser, string fileName)
        {
            var result = parser.Parse(fileSystem.OpenFileStream(fileName));
            EnrichWorkspace(result);
        }

        private void EnrichWorkspace(CodeBase result)
        {
            if (CodeBase == null)
            {
                CodeBase = result;
            }
            else
            {
                CodeBase.Enrich(result.AllInstances);
            }
        }

        private static void OpenFile(Action<string> fileAction, string filter)
        {
            var fileName = GetFileName(filter);
            if (fileName == null) return;
            using (new WaitCursor())
            {
                fileAction(fileName);
            }
        }

        private static string GetFileName(string filter)
        {
            var dialog = new OpenFileDialog {Filter = filter};
            return dialog.ShowDialog().GetValueOrDefault(false) ? dialog.FileName : null;
        }
    }
}