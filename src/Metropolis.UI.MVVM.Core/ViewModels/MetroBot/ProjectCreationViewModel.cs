using Metropolis.UI.MVVM.Core.Models;
using MvvmCross.Core.ViewModels;

namespace Metropolis.UI.MVVM.Core.ViewModels.MetroBot
{
    public class ProjectCreationViewModel : MvxViewModel
    {
        private string name;
        private string sourceDirectory;

        private SourceTypeModel primaryLanguage;

        //eslint
        private EsLintSourceTypeDialectModel dialect;
        private string excludeFile;

        //visual studio
        private string solutionFile;
        private string solutionFolder;


    }
}