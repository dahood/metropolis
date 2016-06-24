using Metropolis.Api.Domain;
using Metropolis.Common.Models;

namespace Metropolis.ViewModels
{
    public class CodeInspectorViewModel
    {
        public string FileName { get; set; }
        public Instance Instance { get; set; }
        public FileContentsResult FileContents { get; set; }
        public RepositorySourceType SourceType { get; set; }
    }
}
