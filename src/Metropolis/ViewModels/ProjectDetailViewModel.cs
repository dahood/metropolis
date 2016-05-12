using System.Collections.Generic;
using System.ComponentModel;
using Metropolis.Common.Extensions;
using Metropolis.Common.Models;

namespace Metropolis.ViewModels
{

    public class ProjectDetailsViewModel : INotifyPropertyChanged
    {
        private string projectName;
        private RepositorySourceType repositorySourceType = RepositorySourceType.Java;  //for now
        private string sourceDirectory;
        private string ignoreFile;
        private string metricsOutputDirectory;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> SourceTypes => new[] { "ECMA" };

        public RepositorySourceType RepositorySourcetype
        {
            get { return repositorySourceType; }
            set
            {
                repositorySourceType = value;
                PropertyChanged.Notify(this, x => x.RepositorySourcetype);
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

        public bool IsValid => ProjectName.IsNotEmpty() &&
                               SourceDirectory.IsNotEmpty() &&
                               MetricsOutputDirectory.IsNotEmpty();

        public string IgnoreFile
        {
            get { return ignoreFile; }
            set
            {
                ignoreFile = value;
                PropertyChanged.Notify(this, x => x.IgnoreFile);
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
