using System.Collections.Generic;
using System.ComponentModel;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.ViewModels
{
    public sealed class ProjectDetailsViewModel : INotifyPropertyChanged
    {
        private string ignoreFile;
        private string metricsOutputDirectory;
        private string projectName = "Sample Project";
        private RepositorySourceType repositorySourceType = RepositorySourceType.Java; //for now
        private string sourceDirectory;
        public IEnumerable<string> SourceTypes => new[] {"ECMA", "Java", "CSharp" };

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
                PropertyChanged.Notify(this, x => x.ProjectName);
            }
        }

        public string SourceDirectory
        {
            get { return sourceDirectory; }
            set
            {
                sourceDirectory = value;
                PropertyChanged.Notify(this, x => x.SourceDirectory);
            }
        }

        public string MetricsOutputDirectory
        {
            get { return metricsOutputDirectory; }
            set
            {
                metricsOutputDirectory = value;
                PropertyChanged.Notify(this, x => x.MetricsOutputDirectory);
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}