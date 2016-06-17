using System;
using System.Collections.Generic;
using Metropolis.Common.Models;
using Metropolis.ViewModels;

namespace Metropolis.Views.UserControls.StepPanels
{
    public interface ICSharpCollectionView
    {
        ProjectDetailsViewModel ProjectDetails { get; }

        event EventHandler BuildRequested;
        event EventHandler<SolutionFileArgs> SolutionFileSelected;
        event EventHandler<CreateIgnoreFileArgs> RunAnalysisRequest; 

        void ShowBuildArtifacts(IEnumerable<FileDto> artifacts);
    }
}