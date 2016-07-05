namespace Metropolis.Api.Domain
{
    public class Duplicate
    {
        private object p;

        public Duplicate(int linesOfCode, int lineNumber, Location location, params Duplicate[] copyCats)
        {
            CopyCats = copyCats;
            LinesOfCode = linesOfCode;
            LineNumber = lineNumber;
            Location = location;
        }

        public int LinesOfCode { get; private set; }
        public int LineNumber { get; private set; }

        public Location Location { get; private set; }

        public Duplicate[] CopyCats { get; private set; }
    }
}