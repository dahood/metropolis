namespace Metropolis.Common.Models
{
    public class MetricsCommandArguments 
    {
        public RepositorySourceType RepositorySourcetype { get; set; }

        public string ProjectName { get; set; }

        public string SourceDirectory { get; set; }
        public string IgnorePath { get; set; }

        public string MetricsOutputDirectory { get; set; }
    }
}
