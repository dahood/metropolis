using System;
using Microsoft.Win32;
using CsvHelper;
using System.IO;
using Metropolis.Api.Core.Analyzers.Toxicity;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Api.Core.Parsers.XmlParsers;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles;
using Metropolis.Api.Service;
using Metropolis.Domain.Camera;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly IMetropolisApi metropolisApi;

        public WorkspaceProvider() : this(new MetropolisApi())
        {
        }

        public WorkspaceProvider(IMetropolisApi metropolisApi)
        {
            this.metropolisApi = metropolisApi;
        }

        public CodeBase Workspace { get; private set; }

        public void Create()
        {
            Workspace = new CodeBase(new CodeGraph(new Class[0]));
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
                metropolisApi.Save(Workspace, dialog.FileName);
            }
        }

        public void Load()
        {
            OpenFile(Load, "Metropolis Project|*.project");
        }

        public void Load(string fileName)
        {
            Workspace = metropolisApi.Load(fileName);  
        }

        public void LoadDefault()
        {
            Workspace = metropolisApi.LoadDefault();
        }

        public void LoadToxicity()
        {
            OpenFile(fileName =>
            {
                var result = metropolisApi.ParseToxicity(fileName, Workspace.SourceBaseDirectory);
                EnrichWorkspace(result);
            }, "Toxicity|*.csv");
        }

        public void LoadVisualStudioMetrics()
        {
            OpenFile(fileName =>
            {
                var result = metropolisApi.ParseVisualStudioMetrics(fileName, Workspace.SourceBaseDirectory);
                EnrichWorkspace(result);
            }, "VisualStudio Metrics|*.csv");
        }
        
        public void LoadCheckStyles()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.Java;
                var parser = CheckStylesParser.JavaCheckStylesParser;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }
        
        public void LoadEsLintCheckStyles()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.ECMA;
                var parser = CheckStylesParser.EslintParser;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }
        public void LoadSourceLinesOfCode(FileInclusion inclusion)
        {
            OpenFile(fileName =>
            {
                var parser = new SourceLinesOfCodeParser(inclusion);
                Parse(parser, fileName);
            }, "Source LOC |*.csv");
        }
        
        public void RunCSharpToxicity()
        {
            Workspace.SourceType = RepositorySourceType.CSharp;
            Workspace = new CSharpToxicityAnalyzer().Analyze(Workspace.AllClasses);
        }

        public void RunJavaToxicity()
        {
            Workspace.SourceType = RepositorySourceType.Java;
            Workspace = new JavaToxicityAnalyzer().Analyze(Workspace.AllClasses);
        }

        public void RunJavascriptToxicity()
        {
            Workspace.SourceType = RepositorySourceType.ECMA;
            Workspace = new JavascriptToxicityAnalyzer().Analyze(Workspace.AllClasses);
        }

        private void Parse(IClassParser parser, string fileName)
        {
            var result = parser.Parse(fileName, Workspace.SourceBaseDirectory);
            EnrichWorkspace(result);
        }

        private void EnrichWorkspace(CodeBase result)
        {
            if (Workspace == null)
            {
                Workspace = result;
            }
            else
            {
                Workspace.Enrich(result.AllClasses);
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
            var dialog = new OpenFileDialog { Filter = filter };
            return dialog.ShowDialog().GetValueOrDefault(false) ? dialog.FileName : null;
        }

        public void RunCsvExport()
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Metropolis CSV export (*.csv)|*.csv",
                AddExtension = true
            };
            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                using (new WaitCursor())
                {
                    using (var stream = new StreamWriter(dialog.FileName))
                    {
                        var writer = new CsvWriter(stream);
                        writer.WriteHeader<Class>();
                        Workspace.AllClasses.ForEach(x => writer.WriteRecord(x));
                    }
                }
            }
        }
    }
}