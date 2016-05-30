using System;
using System.IO;
using System.Reflection.Emit;
using CsvHelper;
using Metropolis.Api.Analyzers.Toxicity;
using Metropolis.Api.Domain;
using Metropolis.Api.Readers;
using Metropolis.Api.Readers.CsvReaders;
using Metropolis.Api.Readers.XmlReaders.CheckStyles;
using Metropolis.Api.Services;
using Metropolis.Camera;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using Microsoft.Win32;

namespace Metropolis
{
    public class WorkspaceProvider : IWorkspaceProvider
    {
        private readonly IAnalysisService analysisService;
        private readonly ICodebaseService codebaseService;

        public WorkspaceProvider() : this(new CodebaseService(), new AnalysisServices())
        {
        }

        private WorkspaceProvider(ICodebaseService codebaseService, IAnalysisService analysisService)
        {
            this.codebaseService = codebaseService;
            this.analysisService = analysisService;
        }

        public CodeBase CodeBase { get; private set; }
        public string MetricsOutputFolder => analysisService.MetricsOutputFolder;
        public bool IsFxcopMetricsInstalled => analysisService.FxCopMetricsPath.IsNotEmpty();

        public void Create()
        {
            CodeBase = new CodeBase(new CodeGraph(new Instance[0]));
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

        public void LoadDefault()
        {
            CodeBase = codebaseService.LoadDefault();
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
                var parser = new SourceLinesOfCodeReader(inclusion);
                Parse(parser, fileName);
            }, "Source LOC |*.csv");
        }

        public void Analyze(ProjectDetailsViewModel viewModel)
        {
            CodeBase.SourceType = viewModel.RepositorySourceType;
            CodeBase = analysisService.Analyze(BuildArguments(viewModel));
        }
        
        private static MetricsCommandArguments BuildArguments(ProjectDetailsViewModel projectDetails)
        {
            return new MetricsCommandArguments
            {
                ProjectName = projectDetails.ProjectName,
                RepositorySourceType = projectDetails.RepositorySourceType,
                IgnoreFile = projectDetails.IgnoreFile,
                SourceDirectory = projectDetails.SourceDirectory
            };
        }

        public void RunCSharpToxicity()
        {
            CodeBase.SourceType = RepositorySourceType.CSharp;
            CodeBase = new CSharpToxicityAnalyzer().Analyze(CodeBase.AllInstances);
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
            var result = parser.Parse(fileName);
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