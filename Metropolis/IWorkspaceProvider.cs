using Metropolis.Domain;
using Metropolis.Parsers.CsvParsers;

namespace Metropolis
{
    public interface IWorkspaceProvider
    {
        CodeBase Workspace { get;  }
        void Create();
        void Save();
        void Load();
        void Load(string fileName);
        void LoadToxicity();
        void LoadVisualStudioMetrics();
        void LoadEclipseMetrics2();
        void LoadMetricsReloadedMethods();
        void LoadMetricsReloadedClasses();
        void RunCSharpToxicity();
        void RunJavaToxicity();
        void RunJavascriptToxicity();
        void RunCsvExport();
        void LoadCheckStyles();
        void LoadEsLintCheckStyles();
        void LoadDefault();
        void LoadSourceLinesOfCode(FileInclusion js);
    }
}