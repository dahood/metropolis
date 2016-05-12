using System.ComponentModel;
using Metropolis.Common.Extensions;

namespace Metropolis.Common.Models
{
    public class ProjectDetails : INotifyPropertyChanged
    {
        private string projectName;
        private string repositorySourceType = RepositorySourceType.Java.ToString();  //for now
        private string cSharpSourceDirectory;
        private string metricsOutputDirectory;

        public event PropertyChangedEventHandler PropertyChanged;

        public string RepositorySourcetype
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

        public string CSharpSourceDirectory
        {
            get { return cSharpSourceDirectory; }
            set
            {
                cSharpSourceDirectory = value;
                PropertyChanged.Notify(this, x => x.CSharpSourceDirectory);
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
