namespace Metropolis.Api.Domain
{
    public class CommitDetail
    {
        public Location Path { get; set; }
        public int AddedLines { get; set; }
        public int DeletedLines { get; set; }

        public bool IsBinary { get; set; }
    }
}