using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers;

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