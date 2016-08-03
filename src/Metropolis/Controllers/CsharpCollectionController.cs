using System;
using System.Collections.Generic;
using System.Linq;
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
            view.SolutionFileSelected += SolutionFileSelected;
            view.BuildRequested += RunBuild;
            view.RunAnalysisRequest += RunAnalysis;
        }
        
        public ProjectDetailsViewModel ProjectDetails => view.ProjectDetails;

        private void RunBuild(object sender, EventArgs e)
        {
            var args = new ProjectBuildArguments(ProjectDetails.ProjectName, ProjectDetails.ProjectFile,
                RepositorySourceType.CSharp, workSpaceProvider.GetProjectBuildFolder(ProjectDetails.ProjectName));

             var buildResult = workSpaceProvider.BuildSolution(args);
            view.ShowBuildArtifacts(Consolidate(ProjectDetails.FilesToIgnore, buildResult.Artifacts));
        }
        
        public static IEnumerable<FileDto> Consolidate(IEnumerable<FileDto> filesToIgnore, IEnumerable<FileDto> artifacts)
        {
            var equalityComparer = new NameComparer();
            return filesToIgnore.Intersect(artifacts, equalityComparer).Union(artifacts.Except(filesToIgnore, equalityComparer)).ToList();
        }

        private void SolutionFileSelected(object sender, SolutionFileArgs e)
        {
            workSpaceProvider.SetUpDotNetBuild(ProjectDetails, e.SolutionFile);
        }

        private void RunAnalysis(object sender, IgnoreFileArgs e)
        {
            ProjectDetails.FilesToIgnore = e.IngoreFiles;
            workSpaceProvider.CreateIgnoreFile(ProjectDetails);
            workSpaceProvider.Analyze(view.ProjectDetails);
        }
    }

    public class NameComparer : IEqualityComparer<FileDto>
    {
        public bool Equals(FileDto x, FileDto y)
        {
            if (ReferenceEquals(null, y)) return false;
            if (ReferenceEquals(x, y)) return true;
            return string.Equals(x.Name, y.Name);
        }

        public int GetHashCode(FileDto obj)
        {
            return obj.Name?.GetHashCode() ?? 0;
        }
    }
}
