namespace Metropolis.Api.Domain
{
    public class Member
    {
        public Member(string name, int linesOfCode, int cylomaticComplexity, int classCoupling)
        {
            Name = name;
            LinesOfCode = linesOfCode;
            CylomaticComplexity = cylomaticComplexity;
            ClassCoupling = classCoupling;
        }

        public string Name { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public int LinesOfCode { get; set; }
        public int CylomaticComplexity { get; set; }
        public int ClassCoupling { get; set; }
        public int NumberOfParameters { get; set; }
        public int MissingDefaultCase { get; set; }
        public int NoFallthrough { get; set; }
        public int BooleanExpressionComplexity { get; set; }
        public int NestedTryDepth { get; set; }
        public int NestedIfDepth { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}
