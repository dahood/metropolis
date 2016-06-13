using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.ViewModels
{
    public sealed class ProjectDetailsViewModel : INotifyPropertyChanged
    {
        private string ignoreFile;
        private string projectName = "Sample Project";
        private RepositorySourceType repositorySourceType = RepositorySourceType.Java; //for now
        private string sourceDirectory;
        private string projectFile;
        public IEnumerable<string> SourceTypes => new[] {"ECMA", "Java", "CSharp"};
        public bool IsFxCopInstalled { get; set; }

        public RepositorySourceType RepositorySourceType
        {
            get { return repositorySourceType; }
            set
            {
                repositorySourceType = value;
                PropertyChanged.Notify(this, x => x.RepositorySourceType);
            }
        }

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                NotifyOfChange(x => x.ProjectName);
            }
        }

        public string ProjectFile
        {
            get { return projectFile; }
            set
            {
                projectFile = value;
                NotifyOfChange(x => x.ProjectFile);
            }
        }

        public bool ProjectFileSelected => projectFile.IsNotEmpty();


        public string SourceDirectory
        {
            get { return sourceDirectory; }
            set
            {
                sourceDirectory = value;
                NotifyOfChange(x => x.SourceDirectory);
            }
        }
        
        public string IgnoreFile
        {
            get { return ignoreFile; }
            set
            {
                ignoreFile = value;
                PropertyChanged.Notify(this, x => x.IgnoreFile);
            }
        }

        public void NotifyOfChange<T>(Expression<Func<ProjectDetailsViewModel, T>> expression)
        {
            PropertyChanged.Notify(this, expression);
            PropertyChanged.Notify(this, x => x.IsValid);
            PropertyChanged.Notify(this, x => x.ProjectFileSelected);
        }

        public bool IsValidForCSharp => projectName.IsNotEmpty() && ProjectFile.IsNotEmpty() && IsFxCopInstalled;
        public bool IsValid => IsForCSharp? projectName.IsNotEmpty() && sourceDirectory.IsNotEmpty() && IsFxCopInstalled 
                                          : projectName.IsNotEmpty() && sourceDirectory.IsNotEmpty();

        public bool IsForCSharp => RepositorySourceType.CSharp == RepositorySourceType;
        public EslintPasringOptions EcmaScriptDialect { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}