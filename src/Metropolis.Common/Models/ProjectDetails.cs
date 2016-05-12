using System;
using System.Collections.Generic;
using System.ComponentModel;
using Metropolis.Common.Extensions;

namespace Metropolis.Common.Models
{
    public class ProjectDetails : INotifyPropertyChanged
    {
        private string projectName;
        private RepositorySourceType repositorySourceType = RepositorySourceType.Java;  //for now
        private string sourceDirectory;
        private string metricsOutputDirectory;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<string> SourceTypes => Enum.GetNames(typeof(RepositorySourceType));

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

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
