﻿namespace Metropolis.Common.Models
{
    public class MetricsCommandArguments
    {
        public RepositorySourceType RepositorySourceType { get; set; }
        public EslintPasringOptions EcmaScriptDialect { get; set; }

        public string ProjectName { get; set; }
        public string ProjectFolder { get; set; }
        public string SourceDirectory { get; set; }
        public string IgnoreFile { get; set; }
        public string MetricsOutputFolder { get; set; }
        public string BuildOutputFolder { get; set; }
        public string ProjectFile { get; set; }
    }
}
