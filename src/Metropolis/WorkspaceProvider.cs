using System;
using Microsoft.Win32;
using CsvHelper;
using System.IO;
using Metropolis.Api.Core.Analyzers.Toxicity;
using Metropolis.Api.Core.Domain;
using Metropolis.Api.Core.Parsers;
using Metropolis.Api.Core.Parsers.CsvParsers;
using Metropolis.Api.Core.Parsers.XmlParsers;
using Metropolis.Api.Core.Parsers.XmlParsers.CheckStyles;
using Metropolis.Api.Microservices;
using Metropolis.Camera;
using Metropolis.Common;
using Metropolis.Common.Models;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly ICodebaseService codebaseService;
        private readonly IProjectService projectService;

        public WorkspaceProvider(ICodebaseService codebaseService, IProjectService projectService)
        {
            this.codebaseService = codebaseService;
            this.projectService = projectService;
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
                codebaseService.Save(Workspace, dialog.FileName);
            }
        }

        public void Load()
        {
            OpenFile(Load, "Metropolis Project|*.project");
        }

        public void Load(string fileName)
        {
            Workspace = codebaseService.Load(fileName);  
        }

        public void LoadDefault()
        {
            Workspace = codebaseService.LoadDefault();
        }

        public void LoadToxicity()
        {
            OpenFile(fileName =>
            {
                var result = codebaseService.Get(fileName, ParseType.RichardToxicity, Workspace.SourceBaseDirectory);
                EnrichWorkspace(result);
            }, "Toxicity|*.csv");
        }

        public void LoadVisualStudioMetrics()
        {
            OpenFile(fileName =>
            {
                var result = codebaseService.Get(fileName, ParseType.VisualStudio, Workspace.SourceBaseDirectory);
                EnrichWorkspace(result);
            }, "VisualStudio Metrics|*.csv");
        }
        
        public void LoadCheckStyles()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.Java;
                var parser = CheckStylesParser.PuppyCrawlParser;
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
            var result = parser.Parse(fileName);
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