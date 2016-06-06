using Metropolis.Api.Domain;
using Metropolis.Api.Readers.CsvReaders;
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
        void LoadToxicity();
        void LoadVisualStudioMetrics();
        void RunCSharpToxicity();
        void RunJavaToxicity();
        void RunJavascriptToxicity();
        void RunCsvExport();
        void LoadCheckStyles();
        void LoadEsLintCheckStyles();
        void LoadDefault();
        void LoadSourceLinesOfCode(FileInclusion js);
        void Analyze(ProjectDetailsViewModel viewModel);

        bool ShowTips { get; set; }
    }
}