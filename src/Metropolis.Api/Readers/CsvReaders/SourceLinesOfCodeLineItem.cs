namespace Metropolis.Api.Readers.CsvReaders
{
    public class SourceLinesOfCodeLineItem
    {
//        Path,Physical,Source,Comment,Single-line comment, Block comment,Mixed,Empty
//        angular.js\src\angular.bind.js,12,7,3,3,0,0,2
        public string Namespace { get; set; }
        public string Class { get; set; }
        public int PhysicalLoc { get; set; }
        public int SourceLoc { get; set; }
        public int CommentLoc { get; set; }
        public int SingleLineCommentLoc { get; set; }
        public int BlockCommentLoc { get; set; }
        public int MixedLoc { get; set; }
        public int EmptyLoc { get; set; }
    }
}