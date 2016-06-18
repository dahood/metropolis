using System;
using System.Collections.Generic;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls.StepPanels
{
    public interface ICSharpCollectionView : ICollectionView
    {
        event EventHandler BuildRequested;
        event EventHandler<SolutionFileArgs> SolutionFileSelected;
        event EventHandler<IgnoreFileArgs> RunAnalysisRequest; 

        void ShowBuildArtifacts(IEnumerable<FileDto> artifacts);
    }

    public interface ICollectionView
    {
        ProjectDetailsViewModel ProjectDetails { get; }
        void RunAnalysis();
    }
}