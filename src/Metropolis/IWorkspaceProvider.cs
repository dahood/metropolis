using Metropolis.Api.Domain;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis
{
    public interface IWorkspaceProvider
    {
        CodeBase CodeBase { get; }
        string MetricsOutputFolder { get; }
        bool IsFxcopMetricsInstalled { get; }
        void Create();
        void Save();
        void Load();
        void Load(string fileName);
        void RunJavaToxicity();
        void RunJavascriptToxicity();
        void RunCsvExport();
        void LoadCheckStyles();
        void LoadEsLintCheckStyles();
        void LoadDefault();
        void LoadSourceLinesOfCode(FileInclusion js);
        void Analyze(ProjectDetailsViewModel viewModel);
        bool ShowTips { get; set; }
        string ScreenShotFolder { get; }
        ProjectBuildResult BuildSolution(ProjectBuildArguments args);
        void CreateIgnoreFile(ProjectDetailsViewModel projectDetails);
        void SetUpDotNetBuild(ProjectDetailsViewModel projectDetails, string solutionFile);
        FileContentsResult GetFileContents(string getPhysicalFilePath);
        void AutoSaveProject(ProjectDetailsViewModel projectDetails);
        void EnsureFolderExists(string screenshotFileName);
        bool AutoloadLastProject();
    }
}