using System;
using System.IO;
using CsvHelper;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Readers;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Services;
using Metropolis.Api.Utilities;
using Metropolis.Camera;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using Microsoft.Win32;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly IAnalysisService analysisService;
        private readonly ICodebaseService codebaseService;
        private readonly IProjectService projectService;
        private readonly IFileSystem fileSystem;

        private readonly string metricsFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Metrics");

        public WorkspaceProvider() : this(new CodebaseService(), new ProjectService(), new AnalysisServices(), new FileSystem())
        {
        }

        public WorkspaceProvider(ICodebaseService codebaseService, IProjectService projectService, IAnalysisService analysisService, IFileSystem fileSystem)
        {
            this.codebaseService = codebaseService;
            this.projectService = projectService;
            this.analysisService = analysisService;
            this.fileSystem = fileSystem;
            fileSystem.CreateFolder(metricsFolder);
        }

        public CodeBase Workspace { get; private set; }

        public void Create()
        {
            Workspace = new CodeBase(new CodeGraph(new Instance[0]));
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
                var result = codebaseService.Get(fileName, ParseType.RichardToxicity);
                EnrichWorkspace(result);
            }, "Toxicity|*.csv");
        }

        public void LoadVisualStudioMetrics()
        {
            OpenFile(fileName =>
            {
                var result = codebaseService.Get(fileName, ParseType.VisualStudio);
                EnrichWorkspace(result);
            }, "VisualStudio Metrics|*.csv");
        }

        public void LoadCheckStyles()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.Java;
                var parser = CheckStylesReader.PuppyCrawlReader;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }

        public void LoadEsLintCheckStyles()
        {
            OpenFile(fileName =>
            {
                Workspace.SourceType = RepositorySourceType.ECMA;
                var parser = CheckStylesReader.EslintReader;
                Parse(parser, fileName);
            }, "Checkstyles |*.xml");
        }

        public void LoadSourceLinesOfCode(FileInclusion inclusion)
        {
            OpenFile(fileName =>
            {
                var parser = new SourceLinesOfCodeReader(inclusion);
                Parse(parser, fileName);
            }, "Source LOC |*.csv");
        }

        public void Analyze(ProjectDetailsViewModel viewModel)
        {
            Workspace.SourceType = viewModel.RepositorySourceType;
            Workspace = analysisService.Analyze(BuildArguments(viewModel));
        }
        
        private MetricsCommandArguments BuildArguments(ProjectDetailsViewModel projectDetails)
        {
            return new MetricsCommandArguments
            {
                ProjectName = projectDetails.ProjectName,
                RepositorySourceType = projectDetails.RepositorySourceType,
                MetricsOutputDirectory = metricsFolder,
                IgnoreFile = projectDetails.IgnoreFile,
                SourceDirectory = projectDetails.SourceDirectory
            };
        }

        public void RunCSharpToxicity()
        {
            Workspace.SourceType = RepositorySourceType.CSharp;
            Workspace = new CSharpToxicityAnalyzer().Analyze(Workspace.AllInstances);
        }

        public void RunJavaToxicity()
        {
            Workspace.SourceType = RepositorySourceType.Java;
            Workspace = new JavaToxicityAnalyzer().Analyze(Workspace.AllInstances);
        }

        public void RunJavascriptToxicity()
        {
            Workspace.SourceType = RepositorySourceType.ECMA;
            Workspace = new JavascriptToxicityAnalyzer().Analyze(Workspace.AllInstances);
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
                    Workspace.AllInstances.ForEach(x => writer.WriteRecord(x));
                }
            }
        }

        private void Parse(IInstanceReader parser, string fileName)
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
                Workspace.Enrich(result.AllInstances);
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