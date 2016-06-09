namespace Metropolis.Common.Models
{
    public class MetricsCommandArguments
    {
        public RepositorySourceType RepositorySourceType { get; set; }
        public EcmaLanguageSetting Setting { get; set; }

        public string ProjectName { get; set; }

        public string SourceDirectory { get; set; }
        public string IgnoreFile { get; set; }
        public string MetricsOutputFolder { get; set; }
    }
}
