namespace Metropolis.Api.Domain
{
    public class Duplicate
    {
        public Duplicate(int linesOfCode, int lineNumber, Location location)
        {
            LinesOfCode = linesOfCode;
            LineNumber = lineNumber;
            Location = location;
        }

        public int LinesOfCode { get; private set; }
        public int LineNumber { get; private set; }

        public Location Location { get; private set; }
    }
}