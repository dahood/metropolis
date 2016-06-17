using System;
using Metropolis.Common.Models;
using Metropolis.ViewModels;
using Metropolis.Views.UserControls.StepPanels;

namespace Metropolis.Controllers
{
    public class CsharpCollectionController
    {
        private readonly ICSharpCollectionView view;
        private readonly IWorkspaceProvider workSpaceProvider;

        public CsharpCollectionController(ICSharpCollectionView view, IWorkspaceProvider workSpaceProvider)
        {
            this.view = view;
            this.workSpaceProvider = workSpaceProvider;
            HookupEvents();
        }

        private void HookupEvents()
        {
            view.BuildRequested += RunBuild;
            view.SolutionFileSelected += SolutionFileSelected;
            view.RunAnalysisRequest += RunAnalysis;
        }
        
        public ProjectDetailsViewModel ProjectDetails => view.ProjectDetails;

        private void RunBuild(object sender, EventArgs e)
        {
            var args = new ProjectBuildArguments
            {
                ProjectName = ProjectDetails.ProjectName,
                ProjetFile = ProjectDetails.ProjectFile,
                SourceType = RepositorySourceType.CSharp
            };

            var buildResult = workSpaceProvider.BuildSolution(args);
            view.ShowBuildArtifacts(buildResult.Artifacts);
        }

        private void SolutionFileSelected(object sender, SolutionFileArgs e)
        {
            workSpaceProvider.SetUpDotNetBuild(ProjectDetails, e.SolutionFile);
        }
        private void RunAnalysis(object sender, CreateIgnoreFileArgs e)
        {
            ProjectDetails.FilesToIgnore = e.IngoreFiles;
            workSpaceProvider.CreateIgnoreFile(ProjectDetails);
            workSpaceProvider.Analyze(view.ProjectDetails);
        }
    }
}
