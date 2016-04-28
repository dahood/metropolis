using System;
using Metropolis.Camera;
using Metropolis.Domain;
using Metropolis.Persistence;
using Microsoft.Win32;
using Metropolis.Parsers.CsvParsers;
using CsvHelper;
using System.IO;
using Metropolis.Parsers.XmlParsers;
using Metropolis.Parsers.XmlParsers.CheckStyles;
using Metropolis.Analyzers.Toxicity;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly ProjectRepository projectRepository = new ProjectRepository();

        public CodeBase Workspace { get; private set; }

        public void Create()
        {
            Workspace = new CodeBase(new CodeGraph(new Class[0]));
        }

        public void Save(string projectname)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Metropolis Project File (*.project)|*.project",
                AddExtension = true
            };
            if (dialog.ShowDialog().GetValueOrDefault(false))
            {
                using (new WaitCursor())
                {
                    Workspace.Name = projectname;
                    projectRepository.Save(Workspace, dialog.FileName);
                }
            }
        }

        public void Load()
        {
            OpenFile(Load, "Metropolis Project|*.project");
        }

        public void Load(string fileName)
        {
            Workspace = projectRepository.Load(fileName);  
        }

        public void LoadDefault()
        {
            Workspace = projectRepository.LoadDefault();
        }

        public void LoadToxicity()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.CSharp;
                var parser = new ToxicityParser();
                Parse(parser, fileName);
            }, "Toxicity|*.csv");
        }

        public void LoadVisualStudioMetrics()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.CSharp;
                var parser = new VisualStudioMetricsParser();
                Parse(parser, fileName);
            }, "VisualStudio Metrics|*.csv");
        }

        public void LoadEclipseMetrics2()
        {
            OpenFile(fileName =>
            {
                var parser = new XmlClassParser();
                Parse(parser, fileName);
            }, "EclipseMetrics2|*.xml");
        }

        public void LoadMetricsReloadedMethods()
        {
            OpenFile(fileName =>
            {
                var parser = new MetricsReloadedMethodParser();
                Parse(parser, fileName);
            }, "MetricsReloaded Methods|*.csv");
        }

        public void LoadMetricsReloadedClasses()
        {
            OpenFile(fileName =>
            {
                var parser = new MetricsReloadedClassParser();
                Parse(parser, fileName);
            }, "MetricsReloaded Class|*.csv");
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
            var result = parser.Parse(fileName);
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
                    var writer = new CsvWriter(new StreamWriter(dialog.FileName));
                    writer.WriteHeader<Class>();
                    Workspace.AllClasses.ForEach(x => writer.WriteRecord(x));
                }
            }
        }
    }
}